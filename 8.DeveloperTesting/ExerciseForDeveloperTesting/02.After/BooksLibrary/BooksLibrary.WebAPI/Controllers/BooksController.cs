using BooksLibrary.Application;
using BooksLibrary.WebAPI.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BooksLibrary.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookUseCases _bookUseCases;

        public BooksController(BookUseCases bookUseCases)
        {
            _bookUseCases = bookUseCases;
        }

        [HttpGet]
        public async Task<ActionResult<List<BookApiDto>>> GetBooks()
        {
            return await _bookUseCases.GetBooks();
        }

        [HttpPost]
        public async Task<ActionResult<BookApiDto>> PostBook(NewBookApiDto newBookDto)
        {
            var addBookResult = await _bookUseCases.AddBook(newBookDto);
            if (addBookResult.IsFailure)
            {
                return BadRequest(addBookResult.Error);
            }

            return Ok(addBookResult.Value);
        }

        [HttpPatch]
        public async Task<ActionResult> MarkBookAsRemoved(Guid bookId)
        {
            var bookMarkingAsRemovedResult = await _bookUseCases.MarkBookAsRemoved(bookId);
            if (bookMarkingAsRemovedResult.IsFailure)
            {
                return BadRequest(bookMarkingAsRemovedResult.Error);
            }

            return Ok();
        }
    }
}
