using DeveloperToolTip.Front.BlazorServer.Models;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace DeveloperToolTip.Front.BlazorServer.Components.Pages.Classifiers
{
    public partial class TopicCategory : ComponentBase
    {
        private IEnumerable<TopicCategoryDto> Categories { get; set; } = Enumerable.Empty<TopicCategoryDto>();

        protected override async Task OnInitializedAsync()
        {
            await LoadCategories();
        }

        private async Task LoadCategories()
        {
            Categories = await _ICategoryService.GetAllCategories();
            StateHasChanged();
        }

        private async Task OpenCreateDialog()
        {
            var result = await DialogService.OpenAsync<CreateCategoryDialog>("Create Category");
            if (result != null)
            {
                var newCategory = new CreateCategoryDto { CategoryName = result.ToString() };
                var success = await _ICategoryService.CreateCategory(newCategory);

                if (success)
                {
                    NotificationService.Notify(NotificationSeverity.Success, "Success", "Category created successfully!");
                    await LoadCategories();
                }
                else
                {
                    NotificationService.Notify(NotificationSeverity.Error, "Error", "Failed to create category");
                }
            }
        }

        private async Task OpenEditDialog(TopicCategoryDto category)
        {
            var result = await DialogService.OpenAsync<EditCategoryDialog>("Edit Category",
                new Dictionary<string, object> { { "Category", category } });

            if (result != null)
            {
                category.CategoryName = result.ToString();
                var success = await _ICategoryService.UpdateCategory(category);

                if (success)
                {
                    NotificationService.Notify(NotificationSeverity.Success, "Success", "Category updated successfully!");
                    await LoadCategories();
                }
                else
                {
                    NotificationService.Notify(NotificationSeverity.Error, "Error", "Failed to update category");
                }
            }
        }

        private async Task OpenDeleteDialog(TopicCategoryDto category)
        {
            var confirm = await DialogService.Confirm("Are you sure you want to delete this category?",
                $"Delete {category.CategoryName}",
                new ConfirmOptions() { OkButtonText = "Yes", CancelButtonText = "No" });

            if (confirm == true)
            {
                var success = await _ICategoryService.DeleteCategory(category.Id);

                if (success)
                {
                    NotificationService.Notify(NotificationSeverity.Success, "Success", "Category deleted successfully!");
                    await LoadCategories();
                }
                else
                {
                    NotificationService.Notify(NotificationSeverity.Error, "Error", "Failed to delete category");
                }
            }
        }

        private void OnRowSelect(TopicCategoryDto category)
        {
            NotificationService.Notify(NotificationSeverity.Info, "Selected", $"Selected category: {category.CategoryName}");
        }
    }
}
