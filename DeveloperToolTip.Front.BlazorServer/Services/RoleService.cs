using DeveloperToolTip.Front.BlazorServer.Models;

namespace DeveloperToolTip.Front.BlazorServer.Services
{
    public class RoleService : IRoleService
    {
        private readonly HttpClient _httpClient;

        public RoleService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        // Roles: Get All
        public async Task<IEnumerable<RoleDto>> GetAllRoles()
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<RoleDto>>("RoleDeveloper");

            return response ?? Enumerable.Empty<RoleDto>();
        }

        public async Task<RoleDto?> GetRoleById(int id)
        {
           var response = await _httpClient.GetFromJsonAsync<RoleDto>($"RoleDeveloper/{id}");
           return response;
        }
    }
}
