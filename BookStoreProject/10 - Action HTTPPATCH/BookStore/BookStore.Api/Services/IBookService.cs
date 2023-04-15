using BookStore.Api.DTO;
using BookStore.Api.Responses;
using BookStore.Repository.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace BookStore.Api.Services
{
    public interface IBookService
    {
        Task<IEnumerable<BookReponseFull>> GetBooksAsync(CancellationToken cancellationToken);
        Task<BookReponseFull?> GetBookByIdAsync(int bookId, CancellationToken cancellationToken);
        Task<int?> AddBookAsync(BookDTO book, CancellationToken cancellationToken);
        Task<Book?> EditBookAsync(int bookId, BookDTO book, CancellationToken cancellationToken);
        Task<bool> PatchBookAsync(int bookId, JsonPatchDocument book, CancellationToken cancellationToken);

        Task<IEnumerable<AuthorResponse>> GetAuthorAsync(CancellationToken cancellationToken);
        Task<AuthorResponse?> GetAuthorByIdAsync(int author, CancellationToken cancellationToken);
        Task<int?> AddAuthorAsync(AuthorDto author, CancellationToken cancellationToken);
        Task<Author?> EditAuthorAsync(int authorId, AuthorDto author, CancellationToken cancellationToken);
        Task<bool> PatchAuthorkAsync(int authorId, JsonPatchDocument authorPatch, CancellationToken cancellationToken);
    }
}
