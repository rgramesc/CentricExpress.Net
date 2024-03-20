namespace BooksLibrary;

public class BookRepository
{
    private readonly List<Book> _books;

    public BookRepository()
    {
        var patternsBookAuthors = new Author[]
        {
            new("Erich", "Gamma"),
            new("Grady", "Booch"),
            new("Richard", "Helm"),
            new("Ralph", "Johnson"),
            new("John", "Vlissides")
        };
        _books = new List<Book>
        {
            new(patternsBookAuthors, new DateOnly(1994, 1, 1),
                "Design Patterns: Elements of Reusable Object-Oriented Software")
        };
    }

    public List<Book> GetBooks()
    {
        return _books;
    }

    public void AddBook(Book book)
    {
        _books.Add(book);
    }
}