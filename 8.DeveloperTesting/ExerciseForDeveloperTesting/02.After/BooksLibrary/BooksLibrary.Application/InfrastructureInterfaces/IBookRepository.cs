using BooksLibrary.Domain;

namespace BooksLibrary.Application.InfrastructureInterfaces;

public interface IBookRepository
{
    Task<List<string>> GetBookTitles();

    Task DeleteBook(Guid bookId);

    Task MarkBookAsRemoved(Book book);
}