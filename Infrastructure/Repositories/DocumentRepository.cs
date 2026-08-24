using Domain.Entity.File;
using Domain.Repostry.file;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public  class DocumentRepository(AppDbContext context) : IDocumentRepository
    {
        public async Task<bool> AddAsync(Document document)
        {
           await context.Documents.AddAsync(document);
         return await  context.SaveChangesAsync()>0;

        }
    }
}
