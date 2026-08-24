using Application.Interfaces.AnswerGemini;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace Infrastructure.Services.Command.file
{
    public class GeminiService : IGeminiService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        public GeminiService(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }
        public async Task<string> GenerateAnswerAsync(string prompt)
        {
            var apiKey = _configuration["Gemini:ApiKey"];
            var url = "https://generativelanguage.googleapis.com/v1beta/models/gemini-3.6-flash:generateContent"; var requestBody = new
            {
                contents = new[]
       {
        new
        {
            parts = new[]
            {
                new
                {
                    text = prompt
                }
            }
        }
    }
            };
            using var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Add("x-goog-api-key", apiKey);
            request.Content = JsonContent.Create(requestBody);

            var response = await _httpClient.SendAsync(request);

            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(
                    $"Status: {(int)response.StatusCode}\nResponse: {responseBody}"
                );
            }
            var result = await response.Content.ReadFromJsonAsync<Response>();
            return result?.Candidates?[0]?.Content?.Parts?[0]?.Text
                   ?? string.Empty;
        }
        private class Response
        {
            [JsonPropertyName("candidates")]
            public Candidate[]? Candidates { get; set; }
        }

        private class Candidate
        {
            [JsonPropertyName("content")]
            public Content? Content { get; set; }
        }

        private class Content
        {
            [JsonPropertyName("parts")]
            public Part[]? Parts { get; set; }
        }

        private class Part
        {
            [JsonPropertyName("text")]
            public string? Text { get; set; }
        }
    }
}
