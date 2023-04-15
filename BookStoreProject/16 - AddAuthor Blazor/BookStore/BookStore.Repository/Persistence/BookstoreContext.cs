using BookStore.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public virtual DbSet<Author> Authors { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("Books");
                entity.Property(e => e.Id)
                .HasMaxLength(200)
                .IsRequired()
                .ValueGeneratedOnAdd();

                entity.Property(e => e.Title)
                .HasMaxLength(200)
                .IsRequired();

                entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsRequired();

               
            });

            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("Authors");
                entity.Property(e => e.Id)
                .HasMaxLength(200)
                .IsRequired()
                .ValueGeneratedOnAdd();

                entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .IsRequired();


                entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsRequired();
            });
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
