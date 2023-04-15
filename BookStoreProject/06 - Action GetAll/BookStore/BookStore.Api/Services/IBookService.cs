using BookStore.Api.Responses;
using BookStore.Repository.Models;

namespace BookStore.Api.Services
{
    public interface IBookService
    {
        Task<IEnumerable<BookReponseFull>> GetBooks(CancellationToken cancellationToken);
    }
}
