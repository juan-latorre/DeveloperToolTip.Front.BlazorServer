using DeveloperToolTip.Front.BlazorServer.Models;

namespace DeveloperToolTip.Front.BlazorServer.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDto>> GetAllRoles();
        Task<RoleDto?> GetRoleById(int id);
        Task<bool> CreateRole(RoleDto role);
        Task<bool> UpdateRole(RoleDto role);
        Task<bool> DeleteRole(int id);
    }
}
