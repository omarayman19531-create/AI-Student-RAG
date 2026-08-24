namespace Application.Interfaces.Entity
{
    public class ChunkingOptions
    {
        public int MaxChunkWords { get; set; } = 350;
        public int OverlapWords { get; set; } = 50;
    }
}
