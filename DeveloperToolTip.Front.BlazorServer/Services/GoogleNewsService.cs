﻿using System.Xml.Linq;
using DeveloperToolTip.Front.BlazorServer.Models;

namespace DeveloperToolTip.Front.BlazorServer.Services
{
    public class GoogleNewsService : IGoogleNewsService
    {
        private readonly HttpClient _httpClient;

        public GoogleNewsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<GoogleNewsDto>> GetNewsAsync(string query)
        {
            var newsList = new List<GoogleNewsDto>();
            try
            {
                // URL del feed RSS de Google News con búsqueda
                var url = $"https://news.google.com/rss/search?q={Uri.EscapeDataString(query)}";

                // Obtener el contenido del feed
                var response = await _httpClient.GetStringAsync(url);

                // Parsear el XML
                var xml = XDocument.Parse(response);
                var imageUrl = xml.Descendants("image").Elements("url").FirstOrDefault()?.Value;
                // Extraer los elementos "item" y mapear al DTO
                foreach (var item in xml.Descendants("item"))
                {
                    
                    var news = new GoogleNewsDto
                    {
                        Title = item.Element("title")?.Value,
                        Link = item.Element("link")?.Value,
                        PublishedDate = item.Element("pubDate")?.Value,
                        ImageUrl = imageUrl 
                    };
                    newsList.Add(news);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching Google News: {ex.Message}");
            }

            return newsList;
        }
    }
}
