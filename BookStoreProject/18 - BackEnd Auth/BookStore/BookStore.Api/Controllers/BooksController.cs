using BookStore.Api.DTO;
using BookStore.Api.Services;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using System;
using System.Threading;

namespace BookStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IOutputCacheStore _cache;
        private readonly IValidator<BookDTO> _validator;
        public BooksController(IBookService bookService, IOutputCacheStore cache, IValidator<BookDTO> validator)
        {
            _bookService = bookService;
            _cache = cache;
            _validator = validator;
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
            ValidationResult validationResult = await _validator.ValidateAsync(bookDto);
            if(!validationResult.IsValid)
            {
                return BadRequest(validationResult.ToDictionary());
            }

            var result = await _bookService.AddBookAsync(bookDto,cancellationToken);
            await _cache.EvictByTagAsync("CachingPolicyBooks", cancellationToken);
            return CreatedAtRoute(new { id = result }, bookDto);
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete( int id, CancellationToken cancellationToken)
        {
            await _bookService.DeleteBookAsync(id, cancellationToken);
            await _cache.EvictByTagAsync("CachingPolicyBooks", cancellationToken);
            return Ok();
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









