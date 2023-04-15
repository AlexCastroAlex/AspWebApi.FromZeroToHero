using BookStore.Repository.Models;

namespace BookStore.Api.Responses
{
    public class BookReponseFull
    {
        public int Id { get; set; }

        public string Title { get; init; } = string.Empty;

        public string Description { get; init; } = string.Empty;

        public  Author? Author { get; set; }
    }
}
