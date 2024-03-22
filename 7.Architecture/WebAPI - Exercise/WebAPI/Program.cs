using Application;
using Application.Books;
using Infrastructure;
using Infrastructure.Persistence;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddSwaggerGen();
            builder.Services.AddInfrastructureServices();
            builder.Services.AddApplicationServices();


            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(Directory.GetCurrentDirectory(), "AppData"));

                var serviceScopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
                using var scope = serviceScopeFactory.CreateScope();
                var booksLibraryDatabaseCreation = scope.ServiceProvider.GetRequiredService<InitDbUseCase>();
                booksLibraryDatabaseCreation.InitDb(false);
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
