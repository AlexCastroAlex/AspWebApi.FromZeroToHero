using BookStore.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repository.Persistence
{
    public class BookstoreContext :DbContext,IBookstoreContext
    {
        public BookstoreContext(DbContextOptions<BookstoreContext> options) : base(options)
        {

        }
        public virtual DbSet<Book> Books { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
