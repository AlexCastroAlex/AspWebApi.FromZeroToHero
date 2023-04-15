using BookStore.Api.DTO;
using BookStore.Api.Services;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace BookStore.Api.Endpoints
{
    public static class BookEndpoint
    {
        public static RouteGroupBuilder MapBookEndpoint(this IEndpointRouteBuilder routes)
        {
            var BookGroup = routes.MapGroup("/api/books").RequireAuthorization();


            //get all
            BookGroup.MapGet("/", async (IBookService _bookService, CancellationToken cancellationToken)
                =>
            { return TypedResults.Ok(await _bookService.GetBooksAsync(cancellationToken)); })
                .Produces(StatusCodes.Status200OK).Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status500InternalServerError)
                .CacheOutput("CachingPolicyBooks");

            //get by id 
            BookGroup.MapGet("/{id}", async (int id, IBookService _bookService, CancellationToken cancellationToken)
               =>
            {
                var result = await _bookService.GetBookByIdAsync(id, cancellationToken);
                if (result is null)
                {
                    return Results.NotFound();
                }
                return TypedResults.Ok(result);
            })
               .Produces(StatusCodes.Status200OK).Produces(StatusCodes.Status400BadRequest)
               .Produces(StatusCodes.Status200OK)
               .Produces(StatusCodes.Status500InternalServerError)
               .CacheOutput("CachingPolicyBooksById");

            // post new author
            BookGroup.MapPost("/", async ([FromBody] BookDTO bookDto, IBookService _bookService, IOutputCacheStore _cache, IValidator<BookDTO> _validator, CancellationToken cancellationToken)
                =>
            {
                ValidationResult validationResult = await _validator.ValidateAsync(bookDto);
                if (!validationResult.IsValid)
                {
                    return TypedResults.BadRequest(validationResult.ToDictionary());
                }

                var result = await _bookService.AddBookAsync(bookDto, cancellationToken);
                await _cache.EvictByTagAsync("CachingPolicyBooks", cancellationToken);
                return Results.Created($"/api/books/{result.ToString()}", bookDto);
            })
                .Produces(StatusCodes.Status200OK).Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status500InternalServerError);

            //delete author
            BookGroup.MapDelete("/{id}", async (int id, IBookService _bookService, IOutputCacheStore _cache, CancellationToken cancellationToken)
               =>
            {
                await _bookService.DeleteBookAsync(id, cancellationToken);
                await _cache.EvictByTagAsync("CachingPolicyBooks", cancellationToken);
                return TypedResults.Ok();
            })
               .Produces(StatusCodes.Status200OK).Produces(StatusCodes.Status400BadRequest)
               .Produces(StatusCodes.Status200OK)
               .Produces(StatusCodes.Status500InternalServerError);

            //Update Author
            BookGroup.MapPut("/{id}", async (int id, IBookService _bookService, [FromBody] BookDTO bookDto, IOutputCacheStore _cache, CancellationToken cancellationToken)
              =>
            {
                var result = await _bookService.EditBookAsync(id, bookDto, cancellationToken);
                if (result is not null)
                {
                    await _cache.EvictByTagAsync("CachingPolicyBooks", cancellationToken);
                    return TypedResults.Ok(result);
                }
                return Results.NotFound();
            })
              .Produces(StatusCodes.Status200OK).Produces(StatusCodes.Status400BadRequest)
              .Produces(StatusCodes.Status200OK)
              .Produces(StatusCodes.Status500InternalServerError);

            //Update partially author
            BookGroup.MapPatch("/{id}", async (int id, IBookService _bookService, [FromBody] JsonPatchDocument bookPatch, IOutputCacheStore _cache, CancellationToken cancellationToken)
             =>
            {
                var result = await _bookService.PatchBookAsync(id, bookPatch, cancellationToken);
                if (result)
                {
                    await _cache.EvictByTagAsync("CachingPolicyBooks", cancellationToken);
                    return TypedResults.Ok();
                }
                return Results.NotFound();
            })
             .Produces(StatusCodes.Status200OK).Produces(StatusCodes.Status400BadRequest)
             .Produces(StatusCodes.Status200OK)
             .Produces(StatusCodes.Status500InternalServerError);


            return BookGroup;
        }
    }
}
