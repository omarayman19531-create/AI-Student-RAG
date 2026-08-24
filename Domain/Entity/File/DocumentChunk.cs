using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.File
{
    public class DocumentChunk
    {
        public Guid Id { get; set; }
        public Guid DocumentId { get; set; }
        public string Content { get; set; }
        public int ChunkIndex { get; set; }
        public float[] Embedding { get; set; }
        public Document document { get; set; }
    }
}
