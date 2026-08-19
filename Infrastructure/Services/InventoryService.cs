using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Services;

public class InventoryService : IInventoryService
{
    private readonly IInventoryRepository _repo;
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly INotificationService _notifier;
    private readonly IAdminDashboardService _dashboardService;
    private readonly IBusinessEventPublisher _eventPublisher;
    private readonly IMemoryCache _cache;

    public InventoryService(
        IInventoryRepository repo, 
        AppDbContext context, 
        IMapper mapper, 
        INotificationService notifier, 
        IAdminDashboardService dashboardService,
        IBusinessEventPublisher eventPublisher,
        IMemoryCache cache)
    {
        _repo = repo;
        _context = context;
        _mapper = mapper;
        _notifier = notifier;
        _dashboardService = dashboardService;
        _eventPublisher = eventPublisher;
        _cache = cache;
    }

    public async Task FinalizeReservationAsync(Guid productId, int quantity, string? performedBy = null)
    {
        var hasActiveTransaction = _context.Database.CurrentTransaction != null;
        using var tx = hasActiveTransaction ? null : await _context.Database.BeginTransactionAsync();
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

            if (tx != null) await tx.CommitAsync();
        }
        catch
        {
            if (tx != null) await tx.RollbackAsync();
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
        if (dto.Quantity < 0)
            throw new ArgumentException("Target stock quantity cannot be negative.");

        // Use transaction for safe updates
        var hasActiveTransaction = _context.Database.CurrentTransaction != null;
        using var tx = hasActiveTransaction ? null : await _context.Database.BeginTransactionAsync();
        try
        {
            var inv = await _repo.GetByProductIdAsync(dto.ProductId);
            int oldStock = inv?.AvailableStock ?? 0;
            int newStock = dto.Quantity;
            int delta = newStock - oldStock;

            if (inv == null)
            {
                inv = new Inventory
                {
                    Id = Guid.NewGuid(),
                    ProductId = dto.ProductId,
                    AvailableStock = newStock,
                    ReservedStock = 0,
                    WarehouseLocation = string.Empty,
                    LowStockThreshold = 5,
                    LastUpdated = DateTime.UtcNow
                };

                await _repo.AddAsync(inv);
            }
            else
            {
                inv.AvailableStock = newStock;
                inv.LastUpdated = DateTime.UtcNow;
                await _repo.UpdateAsync(inv);
            }

            // Sync ProductVariants if present so aggregate product stock matches newStock
            var variants = await _context.ProductVariants.Where(v => v.ProductId == dto.ProductId).ToListAsync();
            if (variants.Any())
            {
                if (variants.Count == 1)
                {
                    variants[0].StockQuantity = newStock;
                }
                else
                {
                    int currentVariantSum = variants.Sum(v => v.StockQuantity);
                    int diff = newStock - currentVariantSum;

                    if (diff > 0)
                    {
                        var primary = variants.FirstOrDefault(v => v.IsActive) ?? variants[0];
                        primary.StockQuantity += diff;
                    }
                    else if (diff < 0)
                    {
                        int remainingToDeduct = Math.Abs(diff);
                        var primary = variants.FirstOrDefault(v => v.IsActive) ?? variants[0];

                        int deductFromPrimary = Math.Min(primary.StockQuantity, remainingToDeduct);
                        primary.StockQuantity -= deductFromPrimary;
                        remainingToDeduct -= deductFromPrimary;

                        foreach (var v in variants.Where(v => v != primary))
                        {
                            if (remainingToDeduct <= 0) break;
                            int deduct = Math.Min(v.StockQuantity, remainingToDeduct);
                            v.StockQuantity -= deduct;
                            remainingToDeduct -= deduct;
                        }

                        if (newStock == 0)
                        {
                            foreach (var v in variants) v.StockQuantity = 0;
                        }
                    }
                }
                await _context.SaveChangesAsync();
            }

            await _repo.AddHistoryAsync(new InventoryHistory
            {
                Id = Guid.NewGuid(),
                InventoryId = inv.Id,
                Change = delta,
                Reason = dto.Reason,
                Timestamp = DateTime.UtcNow,
                PerformedBy = performedBy
            });

            var prod = await _context.Products.FindAsync(dto.ProductId);
            var prodName = prod?.Name ?? "Product";

            string actionType;
            string description;
            string icon;
            string colorClass;
            string bgClass;

            if (delta > 0)
            {
                actionType = "STOCK_INCREASED";
                description = $"Stock increased by {delta} units. Reason: {dto.Reason}. New Stock: {newStock}";
                icon = "PlusCircle";
                colorClass = "text-success";
                bgClass = "bg-success";
            }
            else if (delta < 0)
            {
                actionType = "STOCK_REDUCED";
                description = $"Stock reduced by {Math.Abs(delta)} units. Reason: {dto.Reason}. New Stock: {newStock}";
                icon = "MinusCircle";
                colorClass = "text-warning";
                bgClass = "bg-warning";
            }
            else
            {
                actionType = "STOCK_UPDATED";
                description = $"No stock quantity change occurred. Reason: {dto.Reason}. New Stock: {newStock}";
                icon = "Edit";
                colorClass = "text-info";
                bgClass = "bg-info";
            }

            await _eventPublisher.PublishAsync(new Application.Common.Events.BusinessEvent
            {
                ActionType = actionType,
                EntityType = "Inventory",
                EntityId = dto.ProductId.ToString(),
                Title = prodName,
                Description = description,
                Icon = icon,
                ColorClass = colorClass,
                BgClass = bgClass,
                ProductId = dto.ProductId
            });

            if (newStock == 0)
            {
                await _eventPublisher.PublishAsync(new Application.Common.Events.BusinessEvent
                {
                    ActionType = "ALERT",
                    EntityType = "Inventory",
                    EntityId = dto.ProductId.ToString(),
                    Title = prodName,
                    Description = $"Product '{prodName}' is now Out of Stock!",
                    Icon = "ShieldAlert",
                    ColorClass = "text-danger",
                    BgClass = "bg-danger",
                    ProductId = dto.ProductId
                });
            }
            else if (newStock < inv.LowStockThreshold)
            {
                await _eventPublisher.PublishAsync(new Application.Common.Events.BusinessEvent
                {
                    ActionType = "ALERT",
                    EntityType = "Inventory",
                    EntityId = dto.ProductId.ToString(),
                    Title = prodName,
                    Description = $"Product '{prodName}' has Low Stock! Only {newStock} left.",
                    Icon = "ShieldAlert",
                    ColorClass = "text-warning",
                    BgClass = "bg-warning",
                    ProductId = dto.ProductId
                });
            }

            // Commit transaction after all EF Core operations (inventory update + audit log)
            if (tx != null) await tx.CommitAsync();

            // Invalidate product memory cache so API immediately returns fresh stock
            _cache.Remove("products_all");
            _cache.Remove($"product_{dto.ProductId}");

            if (newStock < inv.LowStockThreshold)
            {
                await _notifier.NotifyAdminLowStockAsync(inv.ProductId, newStock);
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
            if (tx != null) await tx.RollbackAsync();
            throw;
        }
    }

    public async Task ReserveStockAsync(ReserveStockDto dto)
    {
        // Reserve stock during checkout
        var hasActiveTransaction = _context.Database.CurrentTransaction != null;
        using var tx = hasActiveTransaction ? null : await _context.Database.BeginTransactionAsync();
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

            if (tx != null) await tx.CommitAsync();

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
            if (tx != null) await tx.RollbackAsync();
            throw;
        }
    }

    public async Task ReleaseStockAsync(ReserveStockDto dto)
    {
        // Release reserved stock on cancellation
        var hasActiveTransaction = _context.Database.CurrentTransaction != null;
        using var tx = hasActiveTransaction ? null : await _context.Database.BeginTransactionAsync();
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

            if (tx != null) await tx.CommitAsync();
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
