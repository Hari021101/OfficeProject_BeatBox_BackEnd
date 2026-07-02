using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.Extensions.Caching.Memory;

namespace API.Controllers;

//[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly Microsoft.Extensions.Caching.Memory.IMemoryCache _cache;
    private const string ProductsAllCacheKey = "products_all";

    public ProductController(IProductService productService, Microsoft.Extensions.Caching.Memory.IMemoryCache cache)
    {
        _productService = productService;
        _cache = cache;
    }

    [HttpGet]
    [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
    public async Task<IActionResult> GetAllProducts()
    {
        if (!_cache.TryGetValue(ProductsAllCacheKey, out var products))
        {
            products = await _productService.GetAllProductsAsync();
            var cacheEntryOptions = new Microsoft.Extensions.Caching.Memory.MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(5))
                .SetSlidingExpiration(TimeSpan.FromMinutes(1));

            _cache.Set(ProductsAllCacheKey, products, cacheEntryOptions);
        }

        return Ok(products);
    }

    [HttpGet("{id}")]
    [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
    public async Task<IActionResult> GetProductById(Guid id)
    {
        var cacheKey = $"product_{id}";
        if (!_cache.TryGetValue(cacheKey, out var product))
        {
            product = await _productService.GetProductByIdAsync(id);
            if (product == null) return NotFound();

            var cacheEntryOptions = new Microsoft.Extensions.Caching.Memory.MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

            _cache.Set(cacheKey, product, cacheEntryOptions);
        }

        return Ok(product);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddProduct([FromBody] ProductCreateDto productCreateDto)
    {
        var createdProduct = await _productService.AddProductAsync(productCreateDto);
        ClearProductCache(createdProduct.Id);
        return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id }, createdProduct);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] ProductUpdateDto productUpdateDto)
    {
        await _productService.UpdateProductAsync(id, productUpdateDto);
        ClearProductCache(id);
        var updatedProduct = await _productService.GetProductByIdAsync(id);
        return Ok(updatedProduct);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        await _productService.DeleteProductAsync(id);
        ClearProductCache(id);
        return NoContent();
    }

    [HttpPost("bulk-delete")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> BulkDeleteProducts([FromBody] IEnumerable<Guid> productIds)
    {
        await _productService.BulkDeleteAsync(productIds);
        foreach (var id in productIds)
        {
            ClearProductCache(id);
        }
        return NoContent();
    }

    [HttpPost("bulk-feature")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> BulkUpdateFeatured([FromBody] BulkFeatureDto dto)
    {
        await _productService.BulkUpdateFeaturedAsync(dto.ProductIds, dto.IsFeatured);
        foreach (var id in dto.ProductIds)
        {
            ClearProductCache(id);
        }
        return NoContent();
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchProducts([FromQuery] string searchTerm)
    {
        var products = await _productService.SearchProductsAsync(searchTerm);
        return Ok(products);
    }

    [HttpGet("filter")]
    public async Task<IActionResult> FilterProducts([FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice, [FromQuery] string? brand, [FromQuery] string? color)
    {
        var products = await _productService.FilterProductsAsync(minPrice, maxPrice, brand, color);
        return Ok(products);
    }

    [HttpGet("paged")]
    public async Task<IActionResult> GetPagedProducts([FromQuery] int pageNumber, [FromQuery] int pageSize)
    {
        var products = await _productService.GetPagedProductsAsync(pageNumber, pageSize);
        return Ok(products);
    }

    [HttpPost("{productId}/reviews")]
    [Authorize]
    public async Task<IActionResult> AddReview(
    Guid productId,
    AddReviewDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        await _productService.AddReviewAsync(productId, userId, dto);
        ClearProductCache(productId);

        return Ok(new
        {
            message = "Review added successfully"
        });
    }

    private void ClearProductCache(Guid productId)
    {
        _cache.Remove(ProductsAllCacheKey);
        _cache.Remove($"product_{productId}");
    }
}