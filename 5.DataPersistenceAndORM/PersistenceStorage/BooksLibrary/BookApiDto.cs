namespace BooksLibrary;

public class BookApiDto
{
    public AuthorApiDto Author { get; set; }

    public DateTime PublicationDate { get; set; }

    public string Title { get; set; }
}