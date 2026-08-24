using Application.Dto.File;
using Domain.Entity.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.File
{
    public interface IVectorSearchService
    {
       Task< List<ChunkSearchResult>> SearchAsync(
            float[] questionEmbedding,
            int topK,
            float similarityThreshold,Guid documentid);
    }
}
