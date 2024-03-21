using System.Data;
using BooksLibrary.Domain;
using Dapper;

namespace BooksLibrary.DataAccess;

public class BookRepository
{
    private readonly BooksDatabaseTransactionalContext _booksDatabaseContext;

    public BookRepository(BooksDatabaseTransactionalContext booksDatabaseContext)
    {
        _booksDatabaseContext = booksDatabaseContext;
    }

    public async Task AddBook(Book book)
    {
        var query = "INSERT INTO Books (Id, PublicationDate, Title) VALUES (@Id, @PublicationDate, @Title)";
        var parameters = new DynamicParameters();
        parameters.Add("Id", book.Id, DbType.Guid);
        parameters.Add("PublicationDate", book.PublicationDate, DbType.Date);
        parameters.Add("Title", book.Title, DbType.String);

        var connection = _booksDatabaseContext.DatabaseConnection;
        await connection.ExecuteAsync(query, parameters, _booksDatabaseContext.DatabaseTransaction);
    }
}