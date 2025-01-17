using System;
using DeveloperToolTip.Front.BlazorServer.Models;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace DeveloperToolTip.Front.BlazorServer.Components.Pages
{
    public partial class BannerGoogleNews : ComponentBase
    {
        private List<GoogleNewsDto> newsList = new();

        protected override async Task OnInitializedAsync()
        {
            await LoadNewsAsync();
            _ = RefreshNewsPeriodically();
        }

        private async Task LoadNewsAsync()
        {
            var random = new Random();

            try
            {
                var fetchedNews = await _IGoogleNewsService.GetNewsAsync("technology");
                int count = random.Next(1, Math.Min(5, fetchedNews.Count()));

                if (fetchedNews != null && fetchedNews.Any())
                {
                    newsList = fetchedNews.OrderBy(x => random.Next()).Take(count).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching news: {ex.Message}");
            }


            StateHasChanged();
        }

        private async Task RefreshNewsPeriodically()
        {
            while (true)
            {
                await Task.Delay(TimeSpan.FromMinutes(30)); 
                await LoadNewsAsync();
            }
        }

        public IEnumerable<GoogleNewsDto> GetNews()
        {
            return newsList;
        }

    }
}
