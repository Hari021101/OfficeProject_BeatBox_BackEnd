using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
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

    public ImagesController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpDelete("{imageId}")]
    public async Task<IActionResult> DeleteImage(Guid imageId)
    {
        try
        {
            await _productService.DeleteImageAsync(imageId);
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
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
