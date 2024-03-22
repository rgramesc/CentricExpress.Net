namespace Domain
{
    public class Book(Guid id, Author author, DateOnly publicationDate, string title)
    {
        public Book(Author author, DateOnly publicationDate, string title) : this(Guid.NewGuid(), author, publicationDate, title)
        {
        }

        public Guid Id { get; private set; } = id;

        public Author Author { get; private set; } = author;

        public DateOnly PublicationDate { get; private set; } = publicationDate;

        public string Title { get; private set; } = title;
    }
}
