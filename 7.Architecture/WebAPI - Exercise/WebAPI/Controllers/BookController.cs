using Microsoft.AspNetCore.Mvc;

using Application.Books;
using WebApiContracts;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class BookController : ControllerBase
    {
        private BookUseCases _bookUseCases;

        public BookController(BookUseCases bookUseCases)
        {
            this._bookUseCases = bookUseCases;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            return Ok(await _bookUseCases.GetBooks());
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(BookApiDto bookDto)
        {
            return Ok(await _bookUseCases.AddBook(bookDto));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBook(Guid bookId)
        {
            var result = await _bookUseCases.DeleteBook(bookId);
            return result ? Ok() : BadRequest();
        }
    }
}
