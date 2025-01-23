using DeveloperToolTip.Front.BlazorServer.Models;

namespace DeveloperToolTip.Front.BlazorServer.Services
{
    public interface IDeveloperService
    {
        Task<IEnumerable<DeveloperDto>> GetAllRoles();
        Task<DeveloperDto?> GetDevelopersById(int DeveloperId);
    }
}
