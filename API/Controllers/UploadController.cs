using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Microsoft.AspNetCore.Authorization.Authorize(Roles = "Admin")]
public class UploadController : ControllerBase
{
    private readonly IFileUploadService _fileUploadService;

    public UploadController(IFileUploadService fileUploadService)
    {
        _fileUploadService = fileUploadService;
    }

    [HttpPost("product-image")]
    public async Task<IActionResult> UploadProductImage(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest(new ApiResponse<string>(false, "No file was uploaded."));
        }

        using (var stream = file.OpenReadStream())
        {
            var relativePath = await _fileUploadService.UploadProductImageAsync(
                stream,
                file.FileName,
                file.ContentType,
                file.Length
            );

            return Ok(new ApiResponse<string>(true, "Product image uploaded successfully", relativePath));
        }
    }
}
