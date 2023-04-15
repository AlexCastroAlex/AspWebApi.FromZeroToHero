using BookStore.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BooksController(IBookService bookService)
        {
            _bookService= bookService;
        }

        [OutputCache(PolicyName = "CachingPolicy")]
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            return Ok(await _bookService.GetBooksAsync(cancellationToken));
        }

        [OutputCache(PolicyName = "CachingPolicyById")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var result = await _bookService.GetBookByIdAsync(id, cancellationToken);
            if(result is null)
            {
                return NotFound();
            }
            return Ok(result);
        }


    }
}
