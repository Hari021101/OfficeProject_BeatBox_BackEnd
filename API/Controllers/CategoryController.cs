using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _service;
    private readonly Microsoft.Extensions.Caching.Memory.IMemoryCache _cache;
    private const string CategoriesCacheKey = "categories_list";

    public CategoryController(ICategoryService service, Microsoft.Extensions.Caching.Memory.IMemoryCache cache)
    {
        _service = service;
        _cache = cache;
    }

    [AllowAnonymous]
    [HttpGet]
    [ResponseCache(Duration = 300, Location = ResponseCacheLocation.Any)]
    public async Task<IActionResult> GetAll()
    {
        if (!_cache.TryGetValue(CategoriesCacheKey, out var categories))
        {
            categories = await _service.GetAllAsync();
            var cacheEntryOptions = new Microsoft.Extensions.Caching.Memory.MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(10))
                .SetSlidingExpiration(TimeSpan.FromMinutes(2));

            _cache.Set(CategoriesCacheKey, categories, cacheEntryOptions);
        }

        return Ok(categories);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var cacheKey = $"category_{id}";
        if (!_cache.TryGetValue(cacheKey, out var category))
        {
            category = await _service.GetByIdAsync(id);
            if (category == null)
                return NotFound();

            var cacheEntryOptions = new Microsoft.Extensions.Caching.Memory.MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

            _cache.Set(cacheKey, category, cacheEntryOptions);
        }

        return Ok(category);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(
        [FromBody] CategoryCreateDto dto)
    {
        await _service.AddAsync(dto);
        _cache.Remove(CategoriesCacheKey);

        return Ok(new
        {
            Message = "Category created successfully"
        });
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] CategoryUpdateDto dto)
    {
        await _service.UpdateAsync(id, dto);
        _cache.Remove(CategoriesCacheKey);
        _cache.Remove($"category_{id}");

        return Ok(new
        {
            Message = "Category updated successfully"
        });
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);
        _cache.Remove(CategoriesCacheKey);
        _cache.Remove($"category_{id}");

        return Ok(new
        {
            Message = "Category deleted successfully"
        });
    }
}