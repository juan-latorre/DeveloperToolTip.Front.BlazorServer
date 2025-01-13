using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using DeveloperToolTip.Front.BlazorServer.Models;

namespace DeveloperToolTip.Front.BlazorServer.Services
{
    public class APIClientService
    {
        private readonly HttpClient _httpClient;

        public APIClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Auth: Login
        public async Task<string?> LoginAsync(LoginModel loginModel)
        {
            var response = await _httpClient.PostAsJsonAsync("Auth/login", loginModel);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<TokenResponse>();
                return result?.Token;
            }
            return null;
        }

        // Developers: Get All
        public async Task<IEnumerable<DeveloperDto>> GetDevelopersAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<DeveloperDto>>("Developers");
        }

        // Developers: Create
        public async Task<bool> CreateDeveloperAsync(CreateDeveloperDto developer)
        {
            var response = await _httpClient.PostAsJsonAsync("Developers", developer);
            return response.IsSuccessStatusCode;
        }

        // DeveloperRoles: Get All
        public async Task<IEnumerable<RoleDto>> GetRolesAsync()
        {
           return await _httpClient.GetFromJsonAsync<IEnumerable<RoleDto>>("RoleDeveloper");
              
        }

        // TopicCategories: Get All
        public async Task<IEnumerable<TopicCategoryDto>> GetTopicCategoriesAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<TopicCategoryDto>>("TopicCategory");
        }

        // Topics: Get All
        public async Task<IEnumerable<TopicDto>> GetTopicsAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<TopicDto>>("Topic");
        }

        // Topics: Create
        public async Task<bool> CreateTopicAsync(TopicDto topic)
        {
            var response = await _httpClient.PostAsJsonAsync("Topic", topic);
            return response.IsSuccessStatusCode;
        }

        // TopicContents: Get All
        public async Task<IEnumerable<TopicContentDto>> GetTopicContentsAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<TopicContentDto>>("TopicContent");
        }
    }

    // Token Response Model
    public class TokenResponse
    {
        public string? Token { get; set; }
    }
}
