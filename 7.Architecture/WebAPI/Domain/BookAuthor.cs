namespace Domain
{
    public class BookAuthor
    {
        public Guid BookId { get; }
        public Guid AuthorId { get; }

        public BookAuthor(Guid bookId, Guid authorId)
        {
            BookId = bookId;
            AuthorId = authorId;
        }
    }
}
