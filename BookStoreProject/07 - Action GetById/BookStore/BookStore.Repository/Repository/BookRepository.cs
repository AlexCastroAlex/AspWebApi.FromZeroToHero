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

        public BookRepository(IBookstoreContext bookstoreContext)
        {
            _bookstoreContext= bookstoreContext;
        }

        public IQueryable GetBooks()
        {
            return _bookstoreContext.Books.AsNoTracking().AsQueryable();
        }

        public IQueryable GetBookById(int bookId)
        {
            return _bookstoreContext.Books.AsQueryable().Where(c=>c.Id == bookId);
        }

    }
}
