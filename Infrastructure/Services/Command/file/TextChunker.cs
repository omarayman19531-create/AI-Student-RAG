using Application.Interfaces.Entity;
using Application.Interfaces.File;
namespace Infrastructure.Services.Command.file
{
    public  class TextChunker(ChunkingOptions Options) : ITextChunker
    {

        public List<TextChunk> ChunkText(string text)
        {
            var chunks= new List<TextChunk>();
            if(string.IsNullOrWhiteSpace(text))
            {
                return chunks;
            }
            var sentences = SplitIntoSentences(text);
            int chunkIndex = 0;
            var currentwords = new List<string>();
            foreach(var sentence in sentences)
            {
                var sentencewords = sentence.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                if(currentwords.Count+ sentencewords.Length>Options.MaxChunkWords &&currentwords.Count>0)
                {
                    chunks.Add(BuildChunk(currentwords, chunkIndex++));
                    currentwords = currentwords
                       .Skip(Math.Max(0, currentwords.Count - Options.OverlapWords))
                       .ToList();
                }
                currentwords.AddRange(sentencewords); 
            }
            if (currentwords.Count > 0)
                chunks.Add(BuildChunk(currentwords, chunkIndex));
            return chunks;
        }
        private static List<string> SplitIntoSentences(string text)
        {
            var sentences = System.Text.RegularExpressions.Regex.Split(text, @"(?<=[?.!؟])\s+")
                .Where(s => !string.IsNullOrWhiteSpace(s)).ToList();

                return sentences;
        }
        private static TextChunk BuildChunk(List<string>words,int index)
        {
            return new TextChunk
            {
                Index = index,
                Content = string.Join(" ", words),
                WordCount = words.Count,
            };
        }
    }
}
        