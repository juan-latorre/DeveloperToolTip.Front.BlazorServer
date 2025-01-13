using Microsoft.Extensions.DependencyInjection;
using System;

namespace DeveloperToolTip.Front.BlazorServer.Services.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddHttpClients(this IServiceCollection services, IConfiguration configuration)
        {
            var apiBaseUrl = configuration["ApiBaseUrl"];
            if (string.IsNullOrEmpty(apiBaseUrl))
            {
                throw new ArgumentNullException("ApiBaseUrl", "API base URL is not configured.");
            }

            services.AddHttpClient<IAuthService, AuthService>(client =>
            {
                client.BaseAddress = new Uri(apiBaseUrl);
            });

            services.AddHttpClient<IRoleService, RoleService>(client =>
            {
                client.BaseAddress = new Uri(apiBaseUrl);
            });

            services.AddHttpClient<ICategoryService, CategoryService>(client =>
            {
                client.BaseAddress = new Uri(apiBaseUrl);
            });
        }
    }
}
