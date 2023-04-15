using BookStore.Api.Responses;
using BookStore.Api.Services;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BookStore.Api.FEndpoints
{
    public class GetAuthorEndpoint : Endpoint<AuthorRequest, AuthorResponse>
    {
        public override void Configure()
        {
            Get("/api/author/{id}");
            Description(b => b
            .Produces<AuthorResponse>(200, "application/json")
          .ProducesProblemFE<ErrorResponse>(400, "application/json+problem")
          .ProducesProblemFE<InternalErrorResponse>(500));
        }
        private readonly IBookService _bookService;
        public GetAuthorEndpoint(IBookService bookService)
        {
            _bookService = bookService;
        }

        public override async Task HandleAsync(AuthorRequest request, CancellationToken ct)
        {
            await SendOkAsync(await _bookService.GetAuthorByIdAsync(request.id, ct)!);
        }
    }

    public class AuthorRequest
    {
        public int id { get; set; }
    }
}
