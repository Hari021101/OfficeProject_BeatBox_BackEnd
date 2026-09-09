using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces;

public interface IInventoryService
{
    Task<IEnumerable<InventoryDto>> GetAllAsync();
    Task<InventoryDto?> GetByProductIdAsync(Guid productId);
    Task<InventoryDto> UpdateStockAsync(UpdateStockDto dto, string performedBy);
    Task ReserveStockAsync(ReserveStockDto dto);
    Task ReleaseStockAsync(ReserveStockDto dto);
    Task FinalizeReservationAsync(Guid productId, int quantity, string? performedBy = null);
    Task<IEnumerable<InventoryDto>> GetLowStockAsync();
    Task<IEnumerable<InventoryHistory>> GetInventoryLogsAsync();
}
