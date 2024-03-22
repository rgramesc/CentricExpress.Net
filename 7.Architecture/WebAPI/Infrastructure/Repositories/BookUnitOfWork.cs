using Application.Interfaces;
using Domain;
using Infrastructure.Interfaces;

namespace Application.Books
{
    public class BookUnitOfWork: IBookUnitOfWork
    {
        private readonly IBooksDatabaseTransactionalContext _booksDatabaseContext;
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorsRepository _authorsRepository;
        private readonly IBookAuthorRepository _bookAuthorRepository;

        public BookUnitOfWork(
            IBooksDatabaseTransactionalContext booksDatabaseContext,
            IBookRepository bookRepository,
            IAuthorsRepository authorsRepository,
            IBookAuthorRepository bookAuthorRepository)
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
}
