using CentricExpress.OOPPillars.BooksLibrary.Models;

namespace OOPPillars.BooksLibrary
{
    public class BookLibrary
    {
        public IList<Book> Books { get; private set; }

        public BookLibrary()
        {
            Books = new List<Book>();

            //add some hardcoded values in the book collection, for demo purposes
            AddBooks();
        }
        
        public IList<Book> SearchBook(string title)
        { 
            var result = Books.Where(book => book.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();
            return result;
        }

        public IList<Book> SearchBook(string title, string author)
        {
            var result = Books.Where(book => book.Title.Contains(title, StringComparison.OrdinalIgnoreCase) && 
                                             book.Authors.Any(auth => auth.FirstName.Contains(author, StringComparison.OrdinalIgnoreCase) || 
                                                                      auth.LastName.Contains(author, StringComparison.OrdinalIgnoreCase)))
                              .ToList();
            return result ?? new List<Book>();
        }

        private void AddBooks()
        {
            var specialAuthors = new Author[]
                        {
                new Author("Sebastian", "Münster")
                        };

            Book specialBook = new SpecialCollectionBook(specialAuthors, "Cosmografia");
            Books.Add(specialBook);

            var publicAuthors = new Author[]
            {
                new Author("Martin", "Fowler")
            };

            Book publicBook = new PublicBook(publicAuthors, "Patterns of Enterprise Architecture");
            Books.Add(publicBook);
        }

    }
}
