using System.Data;
using BooksLibrary.Application.InfrastructureInterfaces;
using BooksLibrary.Domain;
using BooksLibrary.Infrastructure.Contracts;
using Dapper;

namespace BooksLibrary.Infrastructure;

public class BookAuthorRepository : IBookAuthorRepository
{
    private readonly BooksDatabaseTransactionalContext _booksDatabaseContext;

    public BookAuthorRepository(BooksDatabaseTransactionalContext booksDatabaseContext)
    {
        _booksDatabaseContext = booksDatabaseContext;
    }

    public async Task<List<BookWithAuthorDataDto>> GetBooksWithAuthors()
    {
        var getBooksWithAuthorsSql = @"
SELECT books.Id AS BookId, books.Title AS bookTitle, books.PublicationDate AS bookPublicationDate,
    authors.Id AS authorId, authors.FirstName AS authorFirstName, authors.LastName AS authorLastName
FROM BookAuthor bookAuthors
INNER JOIN Books books on books.Id = bookAuthors.BookId
INNER JOIN Authors authors on authors.Id = bookAuthors.AuthorId";

        var connection = _booksDatabaseContext.DatabaseConnection;
        var booksWithAuthors = await connection.QueryAsync<BookWithAuthorDataDto>(getBooksWithAuthorsSql);
        return booksWithAuthors.ToList();
    }

    public async Task AddBookAuthors(Book book)
    {
        foreach (var author in book.Authors)
        {
            await AddBookAuthor(book.Id, author.Id);
        }
    }

    private async Task AddBookAuthor(Guid bookId, Guid authorId)
    {
        var query = "INSERT INTO BookAuthor (BookId, AuthorId) VALUES (@BookId, @AuthorId)";
        var parameters = new DynamicParameters();
        parameters.Add("BookId", bookId, DbType.Guid);
        parameters.Add("AuthorId", authorId, DbType.Guid);

        var connection = _booksDatabaseContext.DatabaseConnection;
        await connection.ExecuteAsync(query, parameters, _booksDatabaseContext.DatabaseTransaction);
    }
}