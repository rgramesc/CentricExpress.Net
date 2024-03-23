using BooksLibrary.Domain;
using BooksLibrary.Infrastructure.Contracts;

namespace BooksLibrary.Application.InfrastructureInterfaces;

public interface IBookAuthorRepository
{
    Task<List<BookWithAuthorDataDto>> GetBooksWithAuthors();

    Task<List<BookWithAuthorDataDto>> GetBookWithAuthors(Guid guid);

    Task AddBookAuthors(Book book);
}