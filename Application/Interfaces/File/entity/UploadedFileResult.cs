using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.File
{
    public class UploadedFileResult
    {
        public string FilePath { get; set; } = string.Empty;
        public string FileUrl { get; set; } = string.Empty;
    }
}
