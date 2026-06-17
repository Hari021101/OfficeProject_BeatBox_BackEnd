using Application.Interfaces;
using Microsoft.AspNetCore.Hosting;

namespace Infrastructure.Services;

public class FileUploadService : IFileUploadService
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".webp" };
    private static readonly string[] AllowedMimeTypes = { "image/jpeg", "image/png", "image/webp" };
    private const long MaxFileSize = 5 * 1024 * 1024; // 5 MB

    public FileUploadService(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<string> UploadProductImageAsync(Stream fileStream, string fileName, string contentType, long fileSize)
    {
        if (fileStream == null || fileSize == 0)
        {
            throw new ArgumentException("No file content was provided.");
        }

        // 1. File Validation
        // Validate Size
        if (fileSize > MaxFileSize)
        {
            throw new InvalidOperationException("File size exceeds the 5MB limit.");
        }

        // Validate Extension
        var extension = Path.GetExtension(fileName).ToLowerInvariant();
        if (!AllowedExtensions.Contains(extension))
        {
            throw new InvalidOperationException($"Invalid file extension. Supported formats are: {string.Join(", ", AllowedExtensions)}");
        }

        // Validate MIME type
        var mimeType = contentType.ToLowerInvariant();
        if (!AllowedMimeTypes.Contains(mimeType))
        {
            throw new InvalidOperationException("Invalid MIME type. Supported image types are JPEG, PNG, and WEBP.");
        }

        // 2. Storage Management & Directory Creation
        var webRootPath = _webHostEnvironment.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        var uploadsFolder = Path.Combine(webRootPath, "uploads", "products");

        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        // 3. File Naming Strategy (Collision Prevention)
        var uniqueFileName = $"{Guid.NewGuid()}{extension}";
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        // Save file locally
        using (var destinationStream = new FileStream(filePath, FileMode.Create))
        {
            await fileStream.CopyToAsync(destinationStream);
        }

        // Return relative path to access the file
        return $"/uploads/products/{uniqueFileName}";
    }
}
