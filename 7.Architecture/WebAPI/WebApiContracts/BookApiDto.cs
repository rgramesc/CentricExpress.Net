namespace WebApiContracts
{
    public class BookApiDto
    {
        public AuthorApiDto Author { get; set; }

        public DateTime BookPublicationDate { get; set; }

        public string BookTitle { get; set; }
    }
}
