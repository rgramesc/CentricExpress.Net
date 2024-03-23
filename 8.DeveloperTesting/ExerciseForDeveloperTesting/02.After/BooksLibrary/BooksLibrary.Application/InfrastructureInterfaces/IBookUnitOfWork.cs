using BooksLibrary.Domain;

namespace BooksLibrary.Application.InfrastructureInterfaces;

public interface IBookUnitOfWork
{
    Task AddBook(Book book);
}