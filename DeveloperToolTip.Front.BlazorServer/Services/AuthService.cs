using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using DeveloperToolTip.Front.BlazorServer.Models;

namespace DeveloperToolTip.Front.BlazorServer.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
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
            var response = await _httpClient.GetAsync("Auth/active-session");

            if (response.IsSuccessStatusCode)
            {
                var activeSession = await response.Content.ReadFromJsonAsync<DeveloperLogin>();
                return activeSession;
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
