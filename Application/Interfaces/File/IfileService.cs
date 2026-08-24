using Microsoft.AspNetCore.Http;


namespace Application.Interfaces.File
{
    public interface IFileService
    {
        Task<UploadedFileResult> UploadAsync(IFormFile file);
    }
}
