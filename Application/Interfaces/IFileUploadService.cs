namespace Application.Interfaces;

public interface IFileUploadService
{
    Task<string> UploadProductImageAsync(Stream fileStream, string fileName, string contentType, long fileSize);
}
