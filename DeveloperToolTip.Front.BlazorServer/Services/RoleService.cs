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

        // Roles: Create Role
        public async Task<bool> CreateRole(RoleDto role)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("RoleDeveloper", role);
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Roles: Update Role
        public async Task<bool> UpdateRole(RoleDto role)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"RoleDeveloper/{role.Id}", role);
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Roles: Delete Role
        public async Task<bool> DeleteRole(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"RoleDeveloper/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
