using CentricExpress.BooksLibrary.Models;

namespace InMemoryRepository.BooksLibrary.DataAccess
{
    public class BooksRepository
    {
        private IList<Book> _books { get; set; }

        public BooksRepository()
        {
            var authors = new Author[]
            {
                new("Mark", "Seeman")
            };

            _books = new List<Book>
            {
                new(authors, "Dependency Injection in .NET", "Manning Publications"),
            };
        }

        public IList<Book> GetAllBooks()
        {
            return _books;
        }

        public void AddBook(Book book)
        {
            _books.Add(book);
        }
    }
}
