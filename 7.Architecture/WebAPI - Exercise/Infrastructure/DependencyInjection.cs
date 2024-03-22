using Microsoft.Extensions.DependencyInjection;

using Application.Books;
using Application.Interfaces;
using Infrastructure.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Repositories;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IBooksDatabaseTransactionalContext, BooksDatabaseTransactionalContext>();
            
            services.AddScoped<IAuthorsRepository, AuthorsRepository>();
            services.AddScoped<IBookAuthorRepository, BookAuthorRepository>();
            services.AddScoped<IBookRepository, BookRepository>();

            services.AddScoped<IBookUnitOfWork, BookUnitOfWork>();

            services.AddScoped<IBooksLibraryDatabaseCreation, BooksLibraryDatabaseCreation>();
            return services;
        }
    }
}
