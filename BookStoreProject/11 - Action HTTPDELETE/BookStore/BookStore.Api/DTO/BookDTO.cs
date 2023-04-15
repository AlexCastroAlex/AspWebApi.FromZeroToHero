using BookStore.Repository.Models;

namespace BookStore.Api.DTO
{
    public class BookDTO
    {
        public required string Title { get; init; }

        public required string Description { get; init; }

        public int AuthorId { get; set; }    
    }
}
