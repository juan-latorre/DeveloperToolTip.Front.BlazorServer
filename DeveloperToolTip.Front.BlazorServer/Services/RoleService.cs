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

        public async Task<RoleDto?> GetRoleById(int roleId)
        {
           var response = await _httpClient.GetFromJsonAsync<RoleDto>($"RoleDeveloper/{roleId}");
           return response;
        }

        // Roles: Create Role
        public async Task<bool> CreateRole(CreateRoleDto role)
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
                var response = await _httpClient.PutAsJsonAsync($"RoleDeveloper", role);
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Roles: Delete Role
        public async Task<bool> DeleteRole(int roleId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"RoleDeveloper/{roleId}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
