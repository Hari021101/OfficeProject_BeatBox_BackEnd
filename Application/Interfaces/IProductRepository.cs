using Domain.Entities;

namespace Application.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();

    Task<Product?> GetByIdAsync(Guid id);

    Task AddAsync(Product product);

    Task UpdateAsync(Product product);

    Task DeleteAsync(Guid id);

    Task<IEnumerable<Product>> SearchAsync(string searchTerm);

    Task<IEnumerable<Product>> FilterAsync(decimal? minPrice, decimal? maxPrice, string? brand, string? color);

    Task<IEnumerable<Product>> GetPagedAsync(int pageNumber, int pageSize);
}