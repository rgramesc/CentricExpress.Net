using BooksLibrary.Application;
using BooksLibrary.Infrastructure;
using BooksLibrary.WebAPI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseExceptionHandler("/Error");

    var dataDirectoryRelativePath = app.Configuration.GetValue<string>("DataAccess:DataDirectoryRelativePath");
    AppDomain.CurrentDomain.SetData("DataDirectory", Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), dataDirectoryRelativePath)));
    var serviceScopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
    using var scope = serviceScopeFactory.CreateScope();
    var booksLibraryDatabaseCreation = scope.ServiceProvider.GetRequiredService<BooksLibraryDatabaseCreation>();
    var databaseName = app.Configuration.GetValue<string>("DataAccess:DatabaseName");
    await booksLibraryDatabaseCreation.InitiateDatabaseTablesWithData(databaseName);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
