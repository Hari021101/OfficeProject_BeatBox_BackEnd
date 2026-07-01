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
    Task AddReviewAsync(Guid productId, string userId, AddReviewDto dto);
    
    Task BulkDeleteAsync(IEnumerable<Guid> productIds);
    Task BulkUpdateFeaturedAsync(IEnumerable<Guid> productIds, bool isFeatured);

    Task<ProductVariantDto> AddVariantAsync(Guid productId, ProductVariantCreateDto dto);
    Task<ProductVariantDto> UpdateVariantAsync(Guid variantId, ProductVariantUpdateDto dto);
    Task DeleteVariantAsync(Guid variantId);
    Task<List<ProductVariantImageDto>> UploadVariantImagesAsync(Guid variantId, List<(Stream Stream, string FileName, string ContentType, long Length)> files);
    Task DeleteImageAsync(Guid imageId);
    Task ReorderImagesAsync(List<ImageOrderDto> imageOrders);
    Task SetPrimaryImageAsync(Guid imageId);
}