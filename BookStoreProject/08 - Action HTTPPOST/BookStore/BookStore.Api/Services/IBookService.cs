using BookStore.Api.DTO;
using BookStore.Api.Responses;
using BookStore.Repository.Models;

namespace BookStore.Api.Services
{
    public interface IBookService
    {
        Task<IEnumerable<BookReponseFull>> GetBooksAsync(CancellationToken cancellationToken);
        Task<BookReponseFull?> GetBookByIdAsync(int bookId, CancellationToken cancellationToken);
        Task<int?> AddBookAsync(BookDTO book, CancellationToken cancellationToken);

        Task<IEnumerable<AuthorResponse>> GetAuthorAsync(CancellationToken cancellationToken);
        Task<AuthorResponse?> GetAuthorByIdAsync(int author, CancellationToken cancellationToken);
        Task<int?> AddAuthorAsync(AuthorDtoCreation author, CancellationToken cancellationToken);
    }
}
