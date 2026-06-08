using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class InventoryService : IInventoryService
{
    private readonly IInventoryRepository _repo;
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly INotificationService _notifier;
    private readonly IAdminDashboardService _dashboardService;

    public InventoryService(IInventoryRepository repo, AppDbContext context, IMapper mapper, INotificationService notifier, IAdminDashboardService dashboardService)
    {
        _repo = repo;
        _context = context;
        _mapper = mapper;
        _notifier = notifier;
        _dashboardService = dashboardService;
    }

    public async Task FinalizeReservationAsync(Guid productId, int quantity, string? performedBy = null)
    {
        using var tx = await _context.Database.BeginTransactionAsync();
        try
        {
            var inv = await _repo.GetByProductIdAsync(productId);
            if (inv == null) throw new KeyNotFoundException("Inventory record not found.");

            var deduct = Math.Min(quantity, inv.ReservedStock);
            inv.ReservedStock -= deduct;
            // Note: AvailableStock was already decreased at reservation time
            inv.LastUpdated = DateTime.UtcNow;

            await _repo.UpdateAsync(inv);

            await _repo.AddHistoryAsync(new InventoryHistory
            {
                Id = Guid.NewGuid(),
                InventoryId = inv.Id,
                Change = -deduct,
                Reason = "Finalize",
                Timestamp = DateTime.UtcNow,
                PerformedBy = performedBy ?? "system"
            });

            await tx.CommitAsync();
        }
        catch
        {
            await tx.RollbackAsync();
            throw;
        }
    }

    public async Task<IEnumerable<InventoryDto>> GetAllAsync()
    {
        var items = await _repo.GetAllAsync();
        return items.Select(i => _mapper.Map<InventoryDto>(i));
    }

    public async Task<InventoryDto?> GetByProductIdAsync(Guid productId)
    {
        var inv = await _repo.GetByProductIdAsync(productId);
        if (inv == null) return null;
        return _mapper.Map<InventoryDto>(inv);
    }

    public async Task UpdateStockAsync(UpdateStockDto dto, string performedBy)
    {
        // Use transaction for safe updates
        using var tx = await _context.Database.BeginTransactionAsync();
        try
        {
            var inv = await _repo.GetByProductIdAsync(dto.ProductId);
            if (inv == null)
            {
                inv = new Inventory
                {
                    Id = Guid.NewGuid(),
                    ProductId = dto.ProductId,
                    AvailableStock = Math.Max(0, dto.Quantity),
                    ReservedStock = 0,
                    WarehouseLocation = string.Empty,
                    LowStockThreshold = 5,
                    LastUpdated = DateTime.UtcNow
                };

                await _repo.AddAsync(inv);
                await tx.CommitAsync();
                await _repo.AddHistoryAsync(new InventoryHistory
                {
                    Id = Guid.NewGuid(),
                    InventoryId = inv.Id,
                    Change = dto.Quantity,
                    Reason = dto.Reason,
                    Timestamp = DateTime.UtcNow,
                    PerformedBy = performedBy
                });
                return;
            }

            // compute new available stock
            var newAvailable = inv.AvailableStock + dto.Quantity;
            if (newAvailable < 0)
                throw new InvalidOperationException("Available stock cannot go below zero.");

            inv.AvailableStock = newAvailable;
            inv.LastUpdated = DateTime.UtcNow;

            await _repo.UpdateAsync(inv);

            await _repo.AddHistoryAsync(new InventoryHistory
            {
                Id = Guid.NewGuid(),
                InventoryId = inv.Id,
                Change = dto.Quantity,
                Reason = dto.Reason,
                Timestamp = DateTime.UtcNow,
                PerformedBy = performedBy
            });

            await tx.CommitAsync();

            if (inv.AvailableStock < inv.LowStockThreshold)
            {
                // notify admins about low stock
                await _notifier.NotifyAdminLowStockAsync(inv.ProductId, inv.AvailableStock);
                // Best-effort dashboard broadcast
                try
                {
                    var summary = await _dashboardService.GetSummaryAsync();
                    await _notifier.NotifyDashboardUpdatedAsync(summary);
                }
                catch
                {
                }
            }
        }
        catch
        {
            await tx.RollbackAsync();
            throw;
        }
    }

    public async Task ReserveStockAsync(ReserveStockDto dto)
    {
        // Reserve stock during checkout
        using var tx = await _context.Database.BeginTransactionAsync();
        try
        {
            var inv = await _repo.GetByProductIdAsync(dto.ProductId);
            if (inv == null) throw new KeyNotFoundException("Inventory record not found.");

            if (dto.Quantity <= 0) throw new ArgumentException("Quantity must be greater than zero.");

            if (inv.AvailableStock - dto.Quantity < 0)
                throw new InvalidOperationException("Insufficient stock to reserve.");

            inv.AvailableStock -= dto.Quantity;
            inv.ReservedStock += dto.Quantity;
            inv.LastUpdated = DateTime.UtcNow;

            await _repo.UpdateAsync(inv);

            await _repo.AddHistoryAsync(new InventoryHistory
            {
                Id = Guid.NewGuid(),
                InventoryId = inv.Id,
                Change = -dto.Quantity,
                Reason = "Reserve",
                Timestamp = DateTime.UtcNow,
                PerformedBy = dto.UserId ?? "system"
            });

            await tx.CommitAsync();

            if (inv.AvailableStock < inv.LowStockThreshold)
            {
                await _notifier.NotifyAdminLowStockAsync(inv.ProductId, inv.AvailableStock);
                try
                {
                    var summary = await _dashboardService.GetSummaryAsync();
                    await _notifier.NotifyDashboardUpdatedAsync(summary);
                }
                catch
                {
                }
            }
        }
        catch
        {
            await tx.RollbackAsync();
            throw;
        }
    }

    public async Task ReleaseStockAsync(ReserveStockDto dto)
    {
        // Release reserved stock on cancellation
        using var tx = await _context.Database.BeginTransactionAsync();
        try
        {
            var inv = await _repo.GetByProductIdAsync(dto.ProductId);
            if (inv == null) throw new KeyNotFoundException("Inventory record not found.");

            if (dto.Quantity <= 0) throw new ArgumentException("Quantity must be greater than zero.");

            var releaseQty = Math.Min(dto.Quantity, inv.ReservedStock);

            inv.ReservedStock -= releaseQty;
            inv.AvailableStock += releaseQty;
            inv.LastUpdated = DateTime.UtcNow;

            await _repo.UpdateAsync(inv);

            await _repo.AddHistoryAsync(new InventoryHistory
            {
                Id = Guid.NewGuid(),
                InventoryId = inv.Id,
                Change = releaseQty,
                Reason = "Release",
                Timestamp = DateTime.UtcNow,
                PerformedBy = dto.UserId ?? "system"
            });

            await tx.CommitAsync();
            try
            {
                var summary = await _dashboardService.GetSummaryAsync();
                await _notifier.NotifyDashboardUpdatedAsync(summary);
            }
            catch
            {
            }
        }
        catch
        {
            await tx.RollbackAsync();
            throw;
        }
    }

    public async Task<IEnumerable<InventoryDto>> GetLowStockAsync()
    {
        var list = await _repo.GetLowStockAsync();

        return list.Select(i =>
            _mapper.Map<InventoryDto>(i));
    }
    public async Task<IEnumerable<InventoryHistory>> GetInventoryLogsAsync()
    {
        return await _repo.GetInventoryLogsAsync();
    }
}
