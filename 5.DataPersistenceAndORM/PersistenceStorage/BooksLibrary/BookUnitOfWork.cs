﻿namespace BooksLibrary;

public class BookUnitOfWork
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
            await _authorsRepository.AddAuthor(book.Author);
            await _bookAuthorRepository.AddBookAuthor(book);

            _booksDatabaseContext.CommitDatabaseTransaction();
        }
        catch
        {
            _booksDatabaseContext.RollbackDatabaseTransaction();
            throw;
        }
    }
}