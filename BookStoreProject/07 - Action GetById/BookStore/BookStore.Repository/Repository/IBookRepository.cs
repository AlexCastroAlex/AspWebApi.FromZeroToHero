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
    }
}
