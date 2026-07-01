using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers;

[ApiController]
[Route("api/variants")]
[Route("api/variant")]
public class VariantsController : ControllerBase
{
    private readonly IProductService _productService;

    public VariantsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPut("{variantId}")]
    public async Task<IActionResult> UpdateVariant(Guid variantId, [FromBody] ProductVariantUpdateDto dto)
    {
        try
        {
            var updated = await _productService.UpdateVariantAsync(variantId, dto);
            return Ok(updated);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{variantId}")]
    public async Task<IActionResult> DeleteVariant(Guid variantId)
    {
        try
        {
            await _productService.DeleteVariantAsync(variantId);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("{variantId}/images")]
    public async Task<IActionResult> UploadVariantImages(Guid variantId, List<IFormFile> files)
    {
        if (files == null || !files.Any())
        {
            files = Request.Form?.Files?.ToList() ?? new List<IFormFile>();
        }

        if (!files.Any())
        {
            return BadRequest(new { message = "No files were uploaded" });
        }

        var fileDetails = new List<(Stream Stream, string FileName, string ContentType, long Length)>();
        try
        {
            foreach (var file in files)
            {
                fileDetails.Add((file.OpenReadStream(), file.FileName, file.ContentType, file.Length));
            }

            var uploadedImages = await _productService.UploadVariantImagesAsync(variantId, fileDetails);
            return Ok(uploadedImages);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        finally
        {
            foreach (var detail in fileDetails)
            {
                detail.Stream.Dispose();
            }
        }
    }
}
