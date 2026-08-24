namespace Application.Interfaces.File
{
    public class TextChunk
    {
        public int Index { get; set; }
        public string Content { get; set; } = string.Empty;
        public int WordCount { get; set; }
    }
}
