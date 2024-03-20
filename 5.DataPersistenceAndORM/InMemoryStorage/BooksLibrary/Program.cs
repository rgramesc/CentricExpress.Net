using BooksLibrary;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<BookRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/books", (BookRepository bookStore) => bookStore.GetBooks())
    .WithName("GetBooks")
    .WithOpenApi();

app.MapPost("/books", (Book book, BookRepository bookStore) =>
    {
        bookStore.AddBook(book);
        return book;
    })
    .WithName("PostBook")
    .WithOpenApi();

app.Run();