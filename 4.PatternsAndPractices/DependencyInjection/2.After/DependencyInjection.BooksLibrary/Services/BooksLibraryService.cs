using CentricExpress.BooksLibrary.Models;
using InMemoryRepository.BooksLibrary.DataAccess;

namespace InMemoryRepository.BooksLibrary.Services
{
    public class BooksLibraryService
    {
        private BooksRepository _booksRepository { get; set; }
        private AuthorsRepository _authorsRepository { get; set; }

        public BooksLibraryService(BooksRepository booksRepository, AuthorsRepository authorsRepository)
        {
            _booksRepository = booksRepository;
            _authorsRepository = authorsRepository;
        }

        public void AddBook(Book book)
        {
            foreach (var author in book.Authors)
            { 
                _authorsRepository.AddAuthor(author);
            }

            _booksRepository.AddBook(book);
        }

        public IList<Book> GetAllBooks()
        { 
            return _booksRepository.GetAllBooks();
        }

        public IList<Author> GetAllAuthors()
        {
            return _authorsRepository.GetAuthors();
        }
    }
}
