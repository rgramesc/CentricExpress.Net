namespace BooksLibrary.WebAPI.Contracts;

public class NewBookApiDto
{
    public List<NewAuthorApiDto> Authors { get; set; }

    public DateTime PublicationDate { get; set; }

    public string Title { get; set; }
}