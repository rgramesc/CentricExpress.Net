using Domain;

namespace Application.Interfaces
{
    public interface IBookUnitOfWork
    {
        public Task AddBook(Book book);

        public Task<bool> DeleteBook(Guid bookId);
    }
}
