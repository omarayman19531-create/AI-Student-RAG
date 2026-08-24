using Application.Interfaces.File;
//using iTextSharp.text.pdf;
using System.Text;


namespace Infrastructure.Services.Command.file
{
    public class PdfTextExtractor : IPdfTextExtractor
    {
        public async Task<string> ExtractText(string filePath)
        {
            var text = new StringBuilder();
            using var document = UglyToad.PdfPig.PdfDocument.Open(filePath); 
            foreach (var page in document.GetPages())
            {
                text.AppendLine(page.Text);
            }
            return text.ToString();
        }
    }
}
