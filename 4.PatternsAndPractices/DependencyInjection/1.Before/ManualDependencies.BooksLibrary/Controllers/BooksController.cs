using CentricExpress.BooksLibrary.Models;
using InMemoryRepository.BooksLibrary.Services;
using Microsoft.AspNetCore.Mvc;

namespace InMemoryRepository.BooksLibrary.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private BooksLibraryService _libraryService { get; set; }

        public BooksController()
        {
            //initialize library service   
        }

        [HttpGet(Name = "books")]
        public IList<Book> Get()
        {
            return _libraryService.GetAllBooks();
        }

        [HttpPost(Name = "Books")]
        public IActionResult Post(Book book)
        {
            _libraryService.AddBook(book);

            //you can un-comment and  add breakpoints to see that the book and author lists conatin elements that you added
            //var allBooks = _libraryService.GetAllBooks();
            //var allAuthors = _libraryService.GetAllAuthors();

            return Ok(book);
        }
    }
}
