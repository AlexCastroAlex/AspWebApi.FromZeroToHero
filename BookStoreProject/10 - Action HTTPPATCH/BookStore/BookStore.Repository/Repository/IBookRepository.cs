using BookStore.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repository.Repository
{
    public interface IBookRepository
    {
        IQueryable GetBooks();
        IQueryable GetBookById(int bookId);
        Task<int> AddBookAsync(Book book, CancellationToken cancellationToken);
        Task<Book?> EditBookAsync(int bookId, Book book, CancellationToken cancellationToken);
        Task<bool> PatchBookAsync(int bookId, Microsoft.AspNetCore.JsonPatch.JsonPatchDocument bookPatch, CancellationToken cancellationToken);

        IQueryable GetAuthors();
        IQueryable GetAuthorById(int authorId);
        Task<int> AddAuthorAsync(Author author, CancellationToken cancellationToken);
        Task<Author?> EditAuthorAsync(int authorId, Author author, CancellationToken cancellationToken);
        Task<bool> PatchAuthorAsync(int authorId, Microsoft.AspNetCore.JsonPatch.JsonPatchDocument authorPatch, CancellationToken cancellationToken);
    }
}
