using Domain.Entity.File;
using System.Reflection.Metadata;
using Document = Domain.Entity.File.Document;

namespace Domain.Repostry.file
{
    public interface IDocumentRepository
    {
        Task<bool> AddAsync(Document document);
    }
}
