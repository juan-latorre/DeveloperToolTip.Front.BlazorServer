using System.Xml;
using System.Xml.Linq;
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
                //var imageUrl = xml.Descendants("image").Elements("url").FirstOrDefault()?.Value;
                // Extraer los elementos "item" y mapear al DTO
                foreach (var item in xml.Descendants("item"))
                {
                    var sourceElement = item.Element("source");
                    string? sourceUrl = sourceElement?.Attribute("url")?.Value; 
                    string? sourceName = sourceElement?.Value;

                    var news = new GoogleNewsDto
                    {
                        Title = item.Element("title")?.Value,
                        Link = item.Element("link")?.Value,
                        PublishedDate = item.Element("pubDate")?.Value,
                        Source = sourceName,
                        SourceUrl = sourceUrl,
                        ImageUrl = GetClearbitLogo(sourceUrl) ?? GetGoogleFavicon(sourceUrl) //imageUrl
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

        private string? GetClearbitLogo(string? domainUrl)
        {
            if (string.IsNullOrEmpty(domainUrl)) return null;

            try
            {
                var domain = new Uri(domainUrl).Host;
                return $"https://logo.clearbit.com/{domain}";
            }
            catch
            {
                return null;
            }
        }

        private string GetGoogleFavicon(string? domainUrl)
        {
            if (string.IsNullOrEmpty(domainUrl)) return string.Empty;

            try
            {
                var domain = new Uri(domainUrl).Host;
                return $"https://www.google.com/s2/favicons?domain={domain}";
            }
            catch
            {
                return string.Empty;
            }
        }

    }
}
