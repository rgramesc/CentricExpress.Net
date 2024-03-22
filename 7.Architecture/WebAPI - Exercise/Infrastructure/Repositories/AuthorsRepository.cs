using System.Data;
using Dapper;

using Application.Interfaces;
using Domain;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories
{
    public class AuthorsRepository: IAuthorsRepository
    {
        private readonly IBooksDatabaseTransactionalContext _booksDatabaseContext;

        public AuthorsRepository(IBooksDatabaseTransactionalContext booksDatabaseContext)
        {
            _booksDatabaseContext = booksDatabaseContext;
        }

        public async Task<List<Book>> GetBooks()
        {
            var sql = @"SELECT * FROM Books";
            var connection = _booksDatabaseContext.GetDbConnection();
            var books = await connection.QueryAsync<Book>(sql);
            return books.ToList();
        }

        public async Task AddAuthor(Author author)
        {
            var query = "INSERT INTO Authors (Id, FirstName, LastName) VALUES (@Id, @FirstName, @LastName)";
            var parameters = new DynamicParameters();
            parameters.Add("Id", author.Id, DbType.Guid);
            parameters.Add("FirstName", author.FirstName, DbType.String);
            parameters.Add("LastName", author.LastName, DbType.String);

            var connection = _booksDatabaseContext.GetDbConnection();
            await connection.ExecuteAsync(query, parameters, _booksDatabaseContext.GetDbTransaction());
        }
    }
}
