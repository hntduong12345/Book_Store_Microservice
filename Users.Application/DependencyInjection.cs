using Users.Application.Interfaces;
using Users.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Users.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUserApplication(this IServiceCollection services)
        {
            // Register the business logic service
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
