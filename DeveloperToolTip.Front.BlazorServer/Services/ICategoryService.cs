using DeveloperToolTip.Front.BlazorServer.Models;

namespace DeveloperToolTip.Front.BlazorServer.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<TopicCategoryDto>> GetAllCategories();

    }
}
