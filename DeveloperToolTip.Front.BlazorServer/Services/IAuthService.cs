using DeveloperToolTip.Front.BlazorServer.Models;

namespace DeveloperToolTip.Front.BlazorServer.Services
{
    public interface IAuthService
    {
        Task<string?> Login(LoginModel loginModel);
        Task<bool> LogoutAsync(string token);
        Task<DeveloperLogin?> GetActiveSession();
    }
}
