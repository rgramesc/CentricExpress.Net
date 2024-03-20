using BooksLibrary;

var builder = WebApplication.CreateBuilder(args);
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

app.MapGet("/books", async (BookAuthorRepository bookAuthorRepository) =>
    {
        var booksWithAuthors = await bookAuthorRepository.GetBooksWithAuthors();
        var books = new List<Book>();
        booksWithAuthors.ForEach(bookWithAuthor =>
        {
            var author = new Author(bookWithAuthor.AuthorId, bookWithAuthor.AuthorFirstName, bookWithAuthor.AuthorLastName);
            books.Add(new Book(bookWithAuthor.BookId, author, DateOnly.FromDateTime(bookWithAuthor.BookPublicationDate), bookWithAuthor.BookTitle));
        });
        return books;
    })
    .WithName("GetBooks")
    .WithOpenApi();

app.MapPost("/books", async (BookApiDto bookDto, BookUnitOfWork bookUnitOfWork) =>
    {
        var author = new Author(bookDto.Author.FirstName, bookDto.Author.LastName);
        var book = new Book(author, DateOnly.FromDateTime(bookDto.BookPublicationDate), bookDto.BookTitle);
        await bookUnitOfWork.AddBook(book);
        return book;
    })
    .WithName("PostBook")
    .WithOpenApi();

app.Run();