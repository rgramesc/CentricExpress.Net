using Microsoft.Extensions.DependencyInjection;

namespace BooksLibrary.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<BookUseCases>();

            return services;
        }
    }
}
