using BooksLibrary.Domain;

namespace BooksLibrary.Application.InfrastructureInterfaces;

public interface IBookRepository
{
    Task<List<string>> GetBookTitles();
}