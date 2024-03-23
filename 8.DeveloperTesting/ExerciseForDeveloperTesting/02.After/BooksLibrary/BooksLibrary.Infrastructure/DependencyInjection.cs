using BooksLibrary.Application.InfrastructureInterfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BooksLibrary.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<BooksDatabaseTransactionalContext>();
            services.AddScoped<AuthorsRepository>();
            services.AddScoped<BookRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IBookAuthorRepository, BookAuthorRepository>();
            services.AddScoped<BookAuthorRepository>();
            services.AddScoped<IBookUnitOfWork, BookUnitOfWork>();
            services.AddScoped<BookUnitOfWork>();
            services.AddScoped<BooksLibraryDatabaseCreation>();

            return services;
        }
    }
}
