using BookStore.Api.DTO;
using BookStore.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using System.Net;

namespace BookStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IOutputCacheStore _cache;
        public AuthorsController(IBookService bookService, IOutputCacheStore cache)
        {
            _bookService = bookService;
            _cache = cache;
        }

        [OutputCache(PolicyName = "CachingPolicyAuthors")]
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            return Ok(await _bookService.GetAuthorAsync(cancellationToken));
        }

        [OutputCache(PolicyName = "CachingPolicyAuthorsById")]
        [HttpGet("{id}", Name = "GetById")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var result = await _bookService.GetAuthorByIdAsync(id, cancellationToken);
            if (result is null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add([FromBody] AuthorDto authorDTO, CancellationToken cancellationToken)
        {
            var result = await _bookService.AddAuthorAsync(authorDTO, cancellationToken);
            await _cache.EvictByTagAsync("CachingPolicyAuthors", cancellationToken);
            return CreatedAtRoute(new { id = result  }, authorDTO);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Edit(int id,[FromBody] AuthorDto authorDTO, CancellationToken cancellationToken)
        {
            var result = await _bookService.EditAuthorAsync(id, authorDTO, cancellationToken);
            if (result is not null)
            {
                await _cache.EvictByTagAsync("CachingPolicyAuthors", cancellationToken);
                return Ok(result);
            }
            return NotFound();
        }
    }
}
