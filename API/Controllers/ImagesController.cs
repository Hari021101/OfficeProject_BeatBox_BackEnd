using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers;

[ApiController]
[Route("api/images")]
[Route("api/image")]
public class ImagesController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IMemoryCache _cache;
    private const string ProductsAllCacheKey = "products_all";

    public ImagesController(IProductService productService, IMemoryCache cache)
    {
        _productService = productService;
        _cache = cache;
    }

    [HttpDelete("{imageId}")]
    public async Task<IActionResult> DeleteImage(Guid imageId)
    {
        try
        {
            await _productService.DeleteImageAsync(imageId);
            _cache.Remove(ProductsAllCacheKey);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("reorder")]
    public async Task<IActionResult> ReorderImages([FromBody] List<ImageOrderDto> imageOrders)
    {
        try
        {
            await _productService.ReorderImagesAsync(imageOrders);
            _cache.Remove(ProductsAllCacheKey);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{imageId}/primary")]
    public async Task<IActionResult> SetPrimaryImage(Guid imageId)
    {
        try
        {
            await _productService.SetPrimaryImageAsync(imageId);
            _cache.Remove(ProductsAllCacheKey);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
