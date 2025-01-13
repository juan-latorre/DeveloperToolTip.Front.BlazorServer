using DeveloperToolTip.Front.BlazorServer.Models;

namespace DeveloperToolTip.Front.BlazorServer.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;

        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // TopicCategories: Get All Categories
        public async Task<IEnumerable<TopicCategoryDto>> GetAllCategories()
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<TopicCategoryDto>>("TopicCategory");

            return response ?? Enumerable.Empty<TopicCategoryDto>();
        }

    }
}
