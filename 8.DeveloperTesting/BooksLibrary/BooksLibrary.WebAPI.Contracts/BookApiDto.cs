namespace BooksLibrary.WebAPI.Contracts;

public class BookApiDto
{
    public Guid Id { get; set; }

    public List<AuthorApiDto> Authors { get; set; }

    public DateTime PublicationDate { get; set; }

    public string Title { get; set; }
}