using Application.Dto.File;
using Application.Interfaces.File;
using Domain.Entity.File;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Command.file
{
    public class VectorService(AppDbContext context) : IVectorSearchService
    {
        public  async Task< List<ChunkSearchResult>> SearchAsync(float[] questionEmbedding, int topK, float similarityThreshold, Guid documentid)
        {
            var lists=new List<ChunkSearchResult>();
            float dot = 0;
            var chunks =  context.DocumentChunks.Where(a => a.DocumentId == documentid);
            var questionMagnitude = Math.Sqrt(questionEmbedding.Sum(x => x * x));

            foreach (var chunk in chunks)
            {
                var chunkMagnitude = Math.Sqrt(chunk.Embedding.Sum(x => x * x));
                var denominator = chunkMagnitude * questionMagnitude;
                for (int i = 0; i < questionEmbedding.Length; i++)
                {
                    float dotProduct = (chunk.Embedding[i] * questionEmbedding[i]);
                    dot += dotProduct;
                }
                var result = dot / denominator;
                dot = 0;
                lists.Add(new ChunkSearchResult
                {
                    Similarity = result,
                    Content=chunk.Content,
                });
              
            }
            var topResults = lists
    .Where(x => x.Similarity >= similarityThreshold)
    .OrderByDescending(x => x.Similarity)
    .Take(topK)
    .ToList();
            return topResults;
        }
    }
}
