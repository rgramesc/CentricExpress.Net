using Application.Interfaces;

using Domain;
using WebApiContracts;

namespace Application.Books
{
    public class BookUseCases
    {
        private readonly IBookUnitOfWork _bookUnitOfWork;
        private readonly IBookAuthorRepository _bookAuthorRepository;

        public BookUseCases(IBookUnitOfWork bookUnitOfWork, IBookAuthorRepository bookAuthorRepository)
        {
            this._bookAuthorRepository = bookAuthorRepository;
            _bookUnitOfWork = bookUnitOfWork;
        }

        public async Task<List<Book>> GetBooks()
        {
            var booksWithAuthors = await _bookAuthorRepository.GetBooksWithAuthors();
            var books = new List<Book>();
            booksWithAuthors.ForEach(bookWithAuthor =>
            {
                var author = new Author(bookWithAuthor.AuthorId, bookWithAuthor.AuthorFirstName, bookWithAuthor.AuthorLastName);
                books.Add(new Book(bookWithAuthor.BookId, author, DateOnly.FromDateTime(bookWithAuthor.BookPublicationDate), bookWithAuthor.BookTitle));
            });
            return books;
        }

        public async Task<Book> AddBook(BookApiDto bookDto)
        {
            var author = new Author(bookDto.Author.FirstName, bookDto.Author.LastName);
            var book = new Book(author, DateOnly.FromDateTime(bookDto.BookPublicationDate), bookDto.BookTitle);

            await _bookUnitOfWork.AddBook(book);

            return book;
        }

        public async Task<bool> DeleteBook(Guid bookId)
        {
            return await _bookUnitOfWork.DeleteBook(bookId);
        }
    }
}
