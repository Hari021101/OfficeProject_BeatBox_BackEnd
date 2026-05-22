using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        var products = await _productService.GetAllProductsAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(Guid id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null) return NotFound();
        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] ProductCreateDto productCreateDto)
    {
        await _productService.AddProductAsync(productCreateDto);
        return CreatedAtAction(nameof(GetProductById), new { id = productCreateDto.Name }, productCreateDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] ProductUpdateDto productUpdateDto)
    {
        await _productService.UpdateProductAsync(id, productUpdateDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        await _productService.DeleteProductAsync(id);
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
}