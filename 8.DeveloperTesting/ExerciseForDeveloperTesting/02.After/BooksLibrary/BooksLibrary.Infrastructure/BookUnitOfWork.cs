using BooksLibrary.Application.InfrastructureInterfaces;
using BooksLibrary.Domain;

namespace BooksLibrary.Infrastructure;

public class BookUnitOfWork : IBookUnitOfWork
{
    private readonly BooksDatabaseTransactionalContext _booksDatabaseContext;
    private readonly BookRepository _bookRepository;
    private readonly AuthorsRepository _authorsRepository;
    private readonly BookAuthorRepository _bookAuthorRepository;

    public BookUnitOfWork(
        BooksDatabaseTransactionalContext booksDatabaseContext,
        BookRepository bookRepository,
        AuthorsRepository authorsRepository,
        BookAuthorRepository bookAuthorRepository)
    {
        _booksDatabaseContext = booksDatabaseContext;
        _bookRepository = bookRepository;
        _authorsRepository = authorsRepository;
        _bookAuthorRepository = bookAuthorRepository;
    }

    public async Task AddBook(Book book)
    {
        await _booksDatabaseContext.CreateDatabaseTransaction();
        try
        {

            await _bookRepository.AddBook(book);
            foreach (var author in book.Authors)
            {
                await _authorsRepository.AddAuthor(author);
            }
            await _bookAuthorRepository.AddBookAuthors(book);

            _booksDatabaseContext.CommitDatabaseTransaction();
        }
        catch
        {
            _booksDatabaseContext.RollbackDatabaseTransaction();
            throw;
        }
    }
}