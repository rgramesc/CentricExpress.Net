namespace BooksLibrary.Api;

public class BookApiDto
{
    public List<AuthorApiDto> Authors { get; set; }

    public DateTime PublicationDate { get; set; }

    public string Title { get; set; }
}