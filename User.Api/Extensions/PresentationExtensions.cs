namespace Users.Api.Extensions
{
    public static class PresentationExtensions
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();

            return services;
        }
    }
}
