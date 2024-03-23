namespace CentricExpress.OOPPillars.BooksLibrary.Models
{
    public abstract class Book
    {        
        public Author[] Authors { get; set; }
        public string Title { get; set; }

        protected Book(Author[] authors, string title)
        {
            Authors = authors;
            Title = title;
        }        
                
        public abstract string AccessContent(string username);

        public virtual void PrintBook()
        {
            var authors = "";
            foreach (var author in Authors)
            {
                authors += $"{author.LastName}, {author.FirstName}";
            }

            Console.WriteLine($"{Title} - {authors}");
        }

    }
}
