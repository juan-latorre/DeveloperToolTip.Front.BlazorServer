using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using DeveloperToolTip.Front.BlazorServer.Models;
using Blazored.LocalStorage;

namespace DeveloperToolTip.Front.BlazorServer.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public AuthService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        // Auth: Login
        public async Task<string?> Login(LoginModel loginModel)
        {
            var response = await _httpClient.PostAsJsonAsync("Auth/login", loginModel);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<TokenResponse>();
                return result?.Token;
            }
            return null;
        }

        // Auth: Logout
        public async Task LogoutAsync(string token)
        {
            if (string.IsNullOrEmpty(token))
                throw new ArgumentException("Token cannot be null or empty", nameof(token));

            var request = new HttpRequestMessage(HttpMethod.Post, "auth/logout");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Logout failed with status code: {response.StatusCode}");
            }
        }

        // Auth: Get Active Session
        public async Task<DeveloperLogin?> GetActiveSession()
        {
            try
            {
                // Agregar el token de autorización al encabezado
                var request = new HttpRequestMessage(HttpMethod.Get, "Auth/active-session");
                var token = await _localStorage.GetItemAsync<string>("authToken");

                if (!string.IsNullOrEmpty(token))
                {
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                }

                // Realizar la solicitud GET
                var response = await _httpClient.SendAsync(request);

                // Verificar el estado de la respuesta
                if (response.IsSuccessStatusCode)
                {
                    var activeSession = await response.Content.ReadFromJsonAsync<DeveloperLogin>();
                    return activeSession;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    // Manejo de token inválido o expirado
                    Console.WriteLine("Unauthorized: Token might be invalid or expired.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching active session: {ex.Message}");
            }

            return null;
        }


        // Token Response Model
        public class TokenResponse
        {
            public string? Token { get; set; }
        }

    }
}
