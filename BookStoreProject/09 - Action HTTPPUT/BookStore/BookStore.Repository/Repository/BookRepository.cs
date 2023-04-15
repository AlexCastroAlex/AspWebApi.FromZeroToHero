using BookStore.Repository.Models;
using BookStore.Repository.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
            await _bookstoreContext.Books.AddAsync(book, cancellationToken);
            await _bookstoreContext.SaveChangesAsync(cancellationToken);
            return book.Id;
        }

        /// <summary>
        /// modifie un livre dans la base de données
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        public async Task<Book?> EditBookAsync(int bookId, Book book, CancellationToken cancellationToken)
        {
            //var initialBook = await _bookstoreContext.Books.FindAsync(bookId, cancellationToken);
            //if (initialBook is not null)
            //{
            //    initialBook.Title = book.Title;
            //    initialBook.Description = book.Description;
            //    initialBook.AuthorId = book.AuthorId;
            //    await _bookstoreContext.SaveChangesAsync(cancellationToken);
            //}

            //return initialBook;

            var booktoUpdate = new Book()
            {
                Id = bookId,
                Description = book.Description,
                Title = book.Title,
                AuthorId = book.AuthorId
            };
            var bookUpdated = _bookstoreContext.Books.Update(booktoUpdate);
            await _bookstoreContext.SaveChangesAsync();
            return bookUpdated.Entity ?? null;

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
            await _bookstoreContext.Authors.AddAsync(author, cancellationToken); 
            await _bookstoreContext.SaveChangesAsync(cancellationToken);
            return author.Id;
        }

        /// <summary>
        /// modifie un auteur dans la base de données
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        public async Task<Author?> EditAuthorAsync(int authorId, Author author, CancellationToken cancellationToken)
        {
            //var initialauthor = await _bookstoreContext.Authors.FindAsync(authorId, cancellationToken);
            //if (initialauthor is not null)
            //{
            //    initialauthor.FirstName = author.FirstName;
            //    initialauthor.LastName = author.LastName;
            //    await _bookstoreContext.SaveChangesAsync(cancellationToken);
            //}
            var authortoUpdate = new Author
            {
                Id = authorId,
                FirstName = author.FirstName,
                LastName = author.LastName,
            };
            var authorUpdated = _bookstoreContext.Authors.Update(authortoUpdate);

            return authorUpdated.Entity ?? null;
        }


        #endregion
    }
}
