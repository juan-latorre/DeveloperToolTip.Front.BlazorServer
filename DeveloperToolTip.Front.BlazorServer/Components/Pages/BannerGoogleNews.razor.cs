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
            news = await _IGoogleNewsService.GetNewsAsync("technology");
        }

    }
}
