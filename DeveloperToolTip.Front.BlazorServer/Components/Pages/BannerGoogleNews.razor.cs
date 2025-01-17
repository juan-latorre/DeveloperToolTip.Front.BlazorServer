using DeveloperToolTip.Front.BlazorServer.Models;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace DeveloperToolTip.Front.BlazorServer.Components.Pages
{
    public partial class BannerGoogleNews : ComponentBase
    {
        private IEnumerable<GoogleNewsDto>? news;

        protected override async Task OnInitializedAsync()
        {
            await LoadNewsAsync();
            _ = RefreshNewsPeriodically();
        }

        private async Task LoadNewsAsync()
        {
            news = await _IGoogleNewsService.GetNewsAsync("technology");
            StateHasChanged();
        }

        private async Task RefreshNewsPeriodically()
        {
            while (true)
            {
                await Task.Delay(TimeSpan.FromSeconds(5)); 
                await LoadNewsAsync();
            }
        }

    }
}
