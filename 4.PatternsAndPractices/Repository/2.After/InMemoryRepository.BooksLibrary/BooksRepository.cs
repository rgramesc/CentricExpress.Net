using CentricExpress.BooksLibrary.Models;

namespace InMemoryRepository.BooksLibrary
{
    public class BooksRepository
    {
        private IList<Book> _books { get; set; }

        public BooksRepository()
        {
            var patternsBookAuthors = new Author[]
            {
                new("Martin", "Fowler")
            };

            _books = new List<Book>
            {
                new(patternsBookAuthors, "Patterns of Enterprise Application Architecture", "Addison-Wesley Professional")
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
