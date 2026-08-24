using Application.Interfaces.File;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Command.file
{
    public class FileService : IFileService
    {
        public async Task<UploadedFileResult> UploadAsync(IFormFile file)
        {
            var FolderPath =                              // بنحدد ال مسار بتاع الفولدر الهنخزن فيه ا ل cv
                Path.Combine(Directory.GetCurrentDirectory(), // بيجيب مكان المشروع
                "wwwroot",
                "uploads",
                "documents"
                );
            if (!Directory.Exists(FolderPath))
            {
                Directory.CreateDirectory(FolderPath);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(FolderPath, fileName);
            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);
            return new UploadedFileResult
            {
                FilePath = filePath,

                FileUrl = "/uploads/documents/" + fileName
            };
        }
    }
}
