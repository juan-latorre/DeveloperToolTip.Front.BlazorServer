using DeveloperToolTip.Front.BlazorServer.Models;

namespace DeveloperToolTip.Front.BlazorServer.Services
{
    public interface IGoogleNewsService
    {
        Task<IEnumerable<GoogleNewsDto>> GetNewsAsync(string query);
    }
}
