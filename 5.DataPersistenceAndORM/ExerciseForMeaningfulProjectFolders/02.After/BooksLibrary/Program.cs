using BooksLibrary.DataAccess;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<BooksDatabaseTransactionalContext>();
builder.Services.AddScoped<BookRepository>();
builder.Services.AddScoped<AuthorsRepository>();
builder.Services.AddScoped<BookAuthorRepository>();
builder.Services.AddScoped<BookUnitOfWork>();
builder.Services.AddTransient<BooksLibraryDatabaseCreation>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(Directory.GetCurrentDirectory(),"AppData"));
    
    var serviceScopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
    using var scope = serviceScopeFactory.CreateScope();
    var booksLibraryDatabaseCreation = scope.ServiceProvider.GetRequiredService<BooksLibraryDatabaseCreation>();
    await booksLibraryDatabaseCreation.InitiateDatabaseTablesWithData(false);
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();