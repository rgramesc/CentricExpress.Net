namespace BooksLibrary;

public class Book
{
    public Book(Author[] author, DateOnly publicationDate, string title)
    {
        Id = Guid.NewGuid();

        Author = author;
        PublicationDate = publicationDate;
        Title = title;
    }

    public Guid Id { get; private set; }
        
    public Author[] Author { get; private set; }

    public DateOnly PublicationDate { get; private set; }

    public string Title { get; private set; }
}

public record Author(string FirstName, string LastName);