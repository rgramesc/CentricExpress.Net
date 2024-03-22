using Microsoft.Extensions.DependencyInjection;

using Application.Books;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<BookUseCases>();
            services.AddScoped<InitDbUseCase>();
            return services;
        }
    }
}
