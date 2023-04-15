using BookStore.Api.DTO;
using BookStore.Api.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using System.Threading;

namespace BookStore.Api.Endpoints
{
    public static class AuthorEndpoint
    {
        public static RouteGroupBuilder MapAuthorEndpoint(this IEndpointRouteBuilder routes)
        {
            var AuthorGroup = routes.MapGroup("/api/authors").RequireAuthorization();

            //get all
            AuthorGroup.MapGet("/", async (IBookService _bookService, CancellationToken cancellationToken)
                =>
            { return TypedResults.Ok(await _bookService.GetAuthorAsync(cancellationToken)); })
                .Produces(StatusCodes.Status200OK).Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status500InternalServerError)
                .CacheOutput("CachingPolicyAuthors");

            //get by id 
            AuthorGroup.MapGet("/{id}", async (int id, IBookService _bookService, CancellationToken cancellationToken)
               =>
            {
                var result = await _bookService.GetAuthorByIdAsync(id, cancellationToken);
                if (result is null)
                {
                    return Results.NotFound();
                }
                return TypedResults.Ok(result);
            })
               .Produces(StatusCodes.Status200OK).Produces(StatusCodes.Status400BadRequest)
               .Produces(StatusCodes.Status200OK)
               .Produces(StatusCodes.Status500InternalServerError)
               .CacheOutput("CachingPolicyAuthorsById");

            // post new author
            AuthorGroup.MapPost("/", async ([FromBody] AuthorDto authorDTO, IBookService _bookService, IOutputCacheStore _cache, CancellationToken cancellationToken)
                =>
              {
                  var result = await _bookService.AddAuthorAsync(authorDTO, cancellationToken);
                  await _cache.EvictByTagAsync("CachingPolicyAuthors", cancellationToken);
                  return Results.Created($"/api/authors/{result.ToString()}", authorDTO);
              })
                .Produces(StatusCodes.Status200OK).Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status500InternalServerError);

            //delete author
            AuthorGroup.MapDelete("/{id}", async (int id, IBookService _bookService, IOutputCacheStore _cache, CancellationToken cancellationToken)
               =>
            {
                await _bookService.DeleteAuthorAsync(id, cancellationToken);
                await _cache.EvictByTagAsync("CachingPolicyAuthors", cancellationToken);
                return TypedResults.Ok();
            })
               .Produces(StatusCodes.Status200OK).Produces(StatusCodes.Status400BadRequest)
               .Produces(StatusCodes.Status200OK)
               .Produces(StatusCodes.Status500InternalServerError);

            //Update Author
            AuthorGroup.MapPut("/{id}", async (int id, IBookService _bookService,[FromBody] AuthorDto authorDTO, IOutputCacheStore _cache, CancellationToken cancellationToken)
              =>
            {
                var result = await _bookService.EditAuthorAsync(id, authorDTO, cancellationToken);
                if (result is not null)
                {
                    await _cache.EvictByTagAsync("CachingPolicyAuthors", cancellationToken);
                    return TypedResults.Ok(result);
                }
                return Results.NotFound();
            })
              .Produces(StatusCodes.Status200OK).Produces(StatusCodes.Status400BadRequest)
              .Produces(StatusCodes.Status200OK)
              .Produces(StatusCodes.Status500InternalServerError);

            //Update partially author
            AuthorGroup.MapPatch("/{id}", async (int id, IBookService _bookService, [FromBody] JsonPatchDocument authorPatch, IOutputCacheStore _cache, CancellationToken cancellationToken)
             =>
            {
                var result = await _bookService.PatchAuthorkAsync(id, authorPatch, cancellationToken);
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

            return AuthorGroup;
        }
    }
}
