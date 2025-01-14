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

        // TopicCategories: Get Category by ID
        public async Task<TopicCategoryDto?> GetCategoryById(int id)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<TopicCategoryDto>($"TopicCategory/{id}");
                return response;
            }
            catch (Exception)
            {
                return null;
            }
        }

        // TopicCategories: Create Category
        public async Task<bool> CreateCategory(CreateCategoryDto category)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("TopicCategory", category);
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // TopicCategories: Update Category
        public async Task<bool> UpdateCategory(TopicCategoryDto category)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"TopicCategory", category);
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // TopicCategories: Delete Category
        public async Task<bool> DeleteCategory(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"TopicCategory/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

