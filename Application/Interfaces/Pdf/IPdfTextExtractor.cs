using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.File
{
    public  interface IPdfTextExtractor
    {
        Task<string> ExtractText(string filePath);
    }
}
