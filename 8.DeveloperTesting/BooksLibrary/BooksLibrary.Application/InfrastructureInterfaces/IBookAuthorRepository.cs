using BooksLibrary.Domain;
using BooksLibrary.Infrastructure.Contracts;

namespace BooksLibrary.Application.InfrastructureInterfaces;

public interface IBookAuthorRepository
{
    Task<List<BookWithAuthorDataDto>> GetBooksWithAuthors();
    Task AddBookAuthors(Book book);
}