namespace CentricExpress.BooksLibrary.Models
{
    public class Book
    {        
        public Author[] Authors { get; set; }
        public string Title { get; set; }
        public string Publisher { get; set; }

        public Book(Author[] authors, string title, string publisher)
        {           
            Authors = authors;
            Title = title;
            Publisher = publisher;
        }
    }
}
