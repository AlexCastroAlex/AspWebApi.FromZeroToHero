using BookStore.Api.DTO;
using BookStore.Api.Services;
using Microsoft.AspNetCore.JsonPatch;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            return Ok(await _bookService.GetBooksAsync(cancellationToken));
        }

        [OutputCache(PolicyName = "CachingPolicyBooksById")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add([FromBody] BookDTO bookDto, CancellationToken cancellationToken)
        {
            var result = await _bookService.AddBookAsync(bookDto,cancellationToken);
            await _cache.EvictByTagAsync("CachingPolicyBooks", cancellationToken);
            return CreatedAtRoute(new { id = result }, bookDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
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


        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument bookPatch, CancellationToken cancellationToken)
        {
            var result = await _bookService.PatchBookAsync(id, bookPatch, cancellationToken);
            if (result)
            {
                await _cache.EvictByTagAsync("CachingPolicyBooks", cancellationToken);
                return Ok();
            }
            return NotFound();
        }
    }
}









