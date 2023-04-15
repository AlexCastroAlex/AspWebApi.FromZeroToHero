using BookStore.Repository.Models;
using BookStore.Repository.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repository.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly IBookstoreContext _bookstoreContext;

        /// <summary>
        /// Contructeur BookRepository
        /// </summary>
        /// <param name="bookstoreContext"></param>
        public BookRepository(IBookstoreContext bookstoreContext)
        {
            _bookstoreContext= bookstoreContext;
        }
        #region book
        /// <summary>
        /// Récupère tous les livres de la base de données
        /// </summary>
        /// <returns>IQueryble</returns>
        public IQueryable GetBooks()
        {
            return _bookstoreContext.Books.AsNoTracking().AsQueryable();
        }

        /// <summary>
        /// Récupère un livre via son identifiant
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        public IQueryable GetBookById(int bookId)
        {
            return _bookstoreContext.Books.AsQueryable().Where(c=>c.Id == bookId);
        }

        /// <summary>
        /// Rajoute un livre dans la base de données
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        public async Task<int> AddBookAsync(Book book, CancellationToken cancellationToken)
        {
            await _bookstoreContext.Books.AddAsync(book);
            await _bookstoreContext.SaveChangesAsync();
            return book.Id;
        }
        #endregion

        #region authors
        /// <summary>
        /// GetAuthors
        /// </summary>
        /// <returns></returns>
        public IQueryable GetAuthors()
        {
            return _bookstoreContext.Authors.AsNoTracking().AsQueryable();
        }

        /// <summary>
        /// GetAuthorById
        /// </summary>
        /// <returns></returns>
        public IQueryable GetAuthorById(int authorId)
        {
            return _bookstoreContext.Authors.AsQueryable().Where(c => c.Id == authorId);
        }

        /// <summary>
        /// AddAuthor
        /// </summary>
        /// <param name="author"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<int> AddAuthorAsync(Author author, CancellationToken cancellationToken)
        {
            await _bookstoreContext.Authors.AddAsync(author); 
            await _bookstoreContext.SaveChangesAsync();
            return author.Id;
        }
        #endregion
    }
}
