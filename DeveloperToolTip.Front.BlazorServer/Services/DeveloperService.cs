using DeveloperToolTip.Front.BlazorServer.Models;

namespace DeveloperToolTip.Front.BlazorServer.Services
{
    public class DeveloperService : IDeveloperService
    {
        private readonly HttpClient _httpClient;

        public DeveloperService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Developers: Get All Developers
        public async Task<IEnumerable<DeveloperDto>> GetAllRoles()
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<DeveloperDto>>("Developers");

            return response ?? Enumerable.Empty<DeveloperDto>();
        }

        //Get Developer By Id
        public async Task<DeveloperDto?> GetDevelopersById(int DeveloperId)
        {
            var response = await _httpClient.GetFromJsonAsync<DeveloperDto>($"Developers/{DeveloperId}");
            return response;
        }



    }
}
