using DeveloperToolTip.Front.BlazorServer.Models;

namespace DeveloperToolTip.Front.BlazorServer.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<TopicCategoryDto>> GetAllCategories();
        Task<TopicCategoryDto?> GetCategoryById(int id);
        Task<bool> CreateCategory(TopicCategoryDto category);
        Task<bool> UpdateCategory(TopicCategoryDto category);
        Task<bool> DeleteCategory(int id);

    }
}
