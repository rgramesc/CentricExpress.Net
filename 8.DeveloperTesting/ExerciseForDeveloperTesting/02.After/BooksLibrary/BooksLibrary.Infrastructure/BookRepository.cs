using System.Data;
using BooksLibrary.Application.InfrastructureInterfaces;
using BooksLibrary.Domain;
using Dapper;

namespace BooksLibrary.Infrastructure;

public class BookRepository : IBookRepository
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

    public async Task<List<string>> GetBookTitles()
    {
        var getBooksSql = "SELECT Title FROM Books";
        var connection = _booksDatabaseContext.DatabaseConnection;
        var bookTitles = await connection.QueryAsync<string>(getBooksSql);
        return bookTitles.ToList();
    }

    public async Task DeleteBook(Guid bookId)
    {
        var deleteBooksSql = $"DELETE FROM Books WHERE Id = {bookId}";
        var connection = _booksDatabaseContext.DatabaseConnection;
        var parameters = new DynamicParameters();
        parameters.Add("Id", bookId, DbType.Guid);
        await connection.ExecuteAsync(deleteBooksSql, parameters);
    }

    public async Task MarkBookAsRemoved(Book book)
    {
        var deleteBooksSql = "UPDATE Books SET IsRemoved = @IsRemoved WHERE Id = @BookId";
        var connection = _booksDatabaseContext.DatabaseConnection;
        var parameters = new DynamicParameters();
        parameters.Add("Id", book.Id, DbType.Guid);
        parameters.Add("IsRemoved", book.IsRemoved, DbType.Boolean);
        await connection.ExecuteAsync(deleteBooksSql, parameters);
    }
}