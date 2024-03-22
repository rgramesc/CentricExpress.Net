using Domain;

namespace Application.Interfaces
{
    public interface IBookRepository
    {
        public Task AddBook(Book book);

        public Task<bool> DeleteBook(Guid bookId);
    }
}
