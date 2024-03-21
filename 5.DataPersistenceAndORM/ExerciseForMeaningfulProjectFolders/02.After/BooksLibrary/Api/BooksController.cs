using BooksLibrary.DataAccess;
using BooksLibrary.Domain;
using Microsoft.AspNetCore.Mvc;

namespace BooksLibrary.Api
{
    [Route("[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookUnitOfWork _bookUnitOfWork;
        private readonly BookAuthorRepository _bookAuthorRepository;

        public BooksController(BookUnitOfWork bookUnitOfWork, BookAuthorRepository bookAuthorRepository)
        {
            _bookUnitOfWork = bookUnitOfWork;
            _bookAuthorRepository = bookAuthorRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetBooks()
        {
            var booksWithAuthors = await _bookAuthorRepository.GetBooksWithAuthors();
            var books = new List<Book>();
            booksWithAuthors.GroupBy(bookWithAuthor => bookWithAuthor.BookId).ToList().ForEach(bookGroup =>
            {
                var booksInGroup = bookGroup.ToList();
                var firstBookInGroup = booksInGroup.First();
                var authors = new List<Author>();
                booksInGroup.ForEach(bookWithAuthor => authors.Add(new Author(bookWithAuthor.AuthorId, bookWithAuthor.AuthorFirstName, bookWithAuthor.AuthorLastName)));
                books.Add(new Book(firstBookInGroup.BookId, authors, DateOnly.FromDateTime(firstBookInGroup.BookPublicationDate), firstBookInGroup.BookTitle));
            });
            return books;
        }

        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(BookApiDto bookDto)
        {
            var authors = new List<Author>();
            bookDto.Authors.ForEach(author => authors.Add(new Author(author.FirstName, author.LastName)));
            var book = new Book(authors, DateOnly.FromDateTime(bookDto.PublicationDate), bookDto.Title);
            await _bookUnitOfWork.AddBook(book);
            return book;
        }
    }
}
