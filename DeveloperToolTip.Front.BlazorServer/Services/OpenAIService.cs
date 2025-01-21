﻿using System.Net.Http.Headers;

namespace DeveloperToolTip.Front.BlazorServer.Services
{
    public class OpenAIService : IOpenAIService
    {
        private readonly string _apiKey;
        private readonly string _url;
        private readonly HttpClient _httpClient;

        public OpenAIService(string apiKey, string url)
        {
            if (string.IsNullOrWhiteSpace(apiKey)) throw new ArgumentNullException(nameof(apiKey));
            if (string.IsNullOrWhiteSpace(url)) throw new ArgumentNullException(nameof(url));

            _apiKey = apiKey;
            _url = url;

            // Configurar HttpClient
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(_url)
            };

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // Métodos de la clase para interactuar con la API
        public async Task<string> SendRequestAsync(string prompt)
        {
            try
            {
                var requestBody = new
                {
                    model = "gpt-3.5-turbo", // Especifica el modelo a usar
                    messages = new[] { new { role = "user", content = prompt } },
                    max_tokens = 100
                };

                var response = await _httpClient.PostAsJsonAsync("", requestBody);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return content; // Procesar según el esquema de respuesta de OpenAI
                }

                // Manejar respuestas no exitosas
                var errorContent = await response.Content.ReadAsStringAsync();
                var errorCompleteMsg = $"OpenAI API request failed with status code {response.StatusCode}. Details: {errorContent}";
                if (string.IsNullOrEmpty(errorContent))
                {
                    return errorCompleteMsg;
                }
                throw new HttpRequestException($"OpenAI API request failed with status code {response.StatusCode}. Details: {errorContent}");
            }
            catch (HttpRequestException httpEx)
            {
                // Registro detallado para errores de solicitud HTTP
                Console.WriteLine($"HTTP Error: {httpEx.Message}");
                throw; // Re-lanzar la excepción para que sea manejada en niveles superiores si es necesario
            }
            catch (Exception ex)
            {
                // Manejo genérico de errores
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                throw new InvalidOperationException("An error occurred while communicating with OpenAI API.", ex);
            }
        }



    }
}
