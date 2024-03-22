using System.Data;
using Dapper;

using Application.Interfaces;
using Domain;
using Infrastructure.Interfaces;
using InfrastructureContracts;

namespace Infrastructure.Repositories
{
    public class BookAuthorRepository: IBookAuthorRepository
    {
        private readonly IBooksDatabaseTransactionalContext _booksDatabaseContext;

        public BookAuthorRepository(IBooksDatabaseTransactionalContext booksDatabaseContext)
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

            var connection = _booksDatabaseContext.GetDbConnection();
            var booksWithAuthors = await connection.QueryAsync<BookWithAuthorDataDto>(getBooksWithAuthorsSql);
            return booksWithAuthors.ToList();
        }

        public async Task AddBookAuthor(Book book)
        {
            var query = "INSERT INTO BookAuthor (BookId, AuthorId) VALUES (@BookId, @AuthorId)";
            var parameters = new DynamicParameters();
            parameters.Add("BookId", book.Id, DbType.Guid);
            parameters.Add("AuthorId", book.Author.Id, DbType.Guid);

            var connection = _booksDatabaseContext.GetDbConnection();
            await connection.ExecuteAsync(query, parameters, _booksDatabaseContext.GetDbTransaction());
        }
    }
}
