using Domain.Entities;

namespace Application.Interfaces;

public interface IInventoryRepository
{
    Task<Inventory?> GetByProductIdAsync(Guid productId);
    Task<IEnumerable<Inventory>> GetAllAsync();
    Task AddAsync(Inventory inventory);
    Task UpdateAsync(Inventory inventory);
    Task AddHistoryAsync(InventoryHistory history);
    Task<IEnumerable<Inventory>> GetLowStockAsync();
    Task<IEnumerable<InventoryHistory>> GetInventoryLogsAsync();
}

