using Application.Interfaces.Embedding;
using Microsoft.Extensions.Configuration;

using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace Infrastructure.Services.Command.file
{
    public class EmbeddingService : IEmbeddingService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        public EmbeddingService(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }
        public async Task<float[]> GenerateEmbeddingAsync(string chunk)
        {
            var apiKey = _configuration["Gemini:ApiKey"];
            var url = "https://generativelanguage.googleapis.com/v1beta/models/gemini-embedding-2:embedContent";
            var requestBody = new
            {
                model = "models/gemini-embedding-2",
                content = new
                {
                    parts = new[]
                    {
                        new
                        {
                            text = chunk
                        }
                    }

                }
            };
            using var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Add("x-goog-api-key", apiKey);
            request.Content = JsonContent.Create(requestBody);

            var responce = await _httpClient.SendAsync(request);
            responce.EnsureSuccessStatusCode();
            var result = await responce.Content.ReadFromJsonAsync<EmbeddingResponse>();
            return result?.Embedding?.Values ?? Array.Empty<float>();

        }

        private class EmbeddingResponse
        {
            [JsonPropertyName("embedding")]
            public EmbeddingValues? Embedding { get; set; }
        }
        private class EmbeddingValues
        {
            [JsonPropertyName("values")]
            public float[]? Values { get; set; }
        }
    }
}
