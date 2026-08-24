using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.AnswerGemini
{
    public interface IGeminiService
    {
        Task<string> GenerateAnswerAsync(string prompt);
    }
}
