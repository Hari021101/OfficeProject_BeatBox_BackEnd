using Application.DTOs;

namespace Application.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync();
    Task<ProductResponseDto?> GetProductByIdAsync(Guid id);
    Task<ProductResponseDto> AddProductAsync(ProductCreateDto productCreateDto);
    Task UpdateProductAsync(Guid id, ProductUpdateDto productUpdateDto);
    Task DeleteProductAsync(Guid id);
    Task<IEnumerable<ProductResponseDto>> SearchProductsAsync(string searchTerm);
    Task<IEnumerable<ProductResponseDto>> FilterProductsAsync(decimal? minPrice, decimal? maxPrice, string? brand, string? color);
    Task<IEnumerable<ProductResponseDto>> GetPagedProductsAsync(int pageNumber, int pageSize);
}