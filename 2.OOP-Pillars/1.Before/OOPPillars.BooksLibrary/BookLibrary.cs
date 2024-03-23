using CentricExpress.OOPPillars.BooksLibrary.Models;

namespace OOPPillars.BooksLibrary
{
    public class BookLibrary
    {
        public IList<Book> Books { get; private set; }

        public BookLibrary()
        {
            Books = new List<Book>();
            var specialAuthors = new Author[]
            {
                new Author("Sebastian", "Münster")
            };

            Book specialBook = new SpecialCollectionBook(specialAuthors, "Cosmography"); //'Beschreibung aller Länder', or 'A description of all countries'
            Books.Add(specialBook);

            var publicAuthors = new Author[]
            {
                new Author("Martin", "Fowler")
            };

            Book publicBook = new PublicBook(publicAuthors, "Patterns of Enterprise Architecture");
            Books.Add(publicBook);
        }

        public IList<Book> SearchBook(string title)
        {
            //TODO - search in the books list any book containing the given string in the book.Title
            return new List<Book>();
        }

        public IList<Book> SearchBook(string title, string author)
        {
            //TODO - search in the books list any book containing the given values in the book.Title and in book.Authors

            return new List<Book>();
        }        
    }
}
