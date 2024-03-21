namespace BooksLibrary.Domain;

public class Book(Guid id, List<Author> authors, DateOnly publicationDate, string title)
{
    public Book(List<Author> authors, DateOnly publicationDate, string title) : this(Guid.NewGuid(), authors, publicationDate, title)
    {
    }

    public Guid Id { get; private set; } = id;

    public List<Author> Authors { get; private set; } = authors;

    public DateOnly PublicationDate { get; private set; } = publicationDate;

    public string Title { get; private set; } = title;
}