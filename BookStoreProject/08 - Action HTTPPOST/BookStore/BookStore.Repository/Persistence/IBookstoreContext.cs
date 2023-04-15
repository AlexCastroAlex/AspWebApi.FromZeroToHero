using BookStore.Repository.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repository.Persistence
{
    public interface IBookstoreContext
    {
        DatabaseFacade Database { get; }
        DbSet<Book> Books { get; set; }
        DbSet<Author> Authors { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
