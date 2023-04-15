using BookStore.Api.Responses;
using BookStore.Api.Services;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BookStore.Api.FEndpoints
{
    public class GetAuthorsEndpoint : EndpointWithoutRequest<IEnumerable<AuthorResponse>>
    {
        public override void Configure()
        {
            Get("/api/authors");
            Description(b => b
            .Produces<IEnumerable<AuthorResponse>>(200,"application/json")
          .ProducesProblemFE<ErrorResponse>(400, "application/json+problem")
          .ProducesProblemFE<InternalErrorResponse>(500)); //if using FE exception handler
        }
        private readonly IBookService _bookService;
        public GetAuthorsEndpoint(IBookService bookService)
        {
            _bookService= bookService;
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            await SendOkAsync(await _bookService.GetAuthorAsync(ct));
        }
    }
}
