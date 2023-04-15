using BookStore.Api.DTO;
using BookStore.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using System.Threading;

namespace BookStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IOutputCacheStore _cache;
        public BooksController(IBookService bookService, IOutputCacheStore cache)
        {
            _bookService = bookService;
            _cache = cache; 
        }

        [OutputCache(PolicyName = "CachingPolicyBooks")]
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            return Ok(await _bookService.GetBooksAsync(cancellationToken));
        }

        [OutputCache(PolicyName = "CachingPolicyBooksById")]
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

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] BookDTO bookDto, CancellationToken cancellationToken)
        {
            var result = await _bookService.AddBookAsync(bookDto,cancellationToken);
            await _cache.EvictByTagAsync("CachingPolicyBooks", cancellationToken);
            return CreatedAtRoute(new { id = result }, bookDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Edit(int id ,[FromBody] BookDTO bookDto, CancellationToken cancellationToken)
        {
            var result = await _bookService.EditBookAsync(id, bookDto, cancellationToken);
            if (result is not null)
            {
                await _cache.EvictByTagAsync("CachingPolicyBooks", cancellationToken);
                return Ok(result);
            }
            return NotFound();
        }
    }
}









