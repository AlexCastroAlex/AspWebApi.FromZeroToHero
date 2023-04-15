namespace Bookstore.FrontEnd.DTO
{
    public class BookDto
    {
        public int Id { get; set; }

        public string Title { get; init; } = string.Empty;

        public string Description { get; init; } = string.Empty;

        public AuthorDto? Author { get; set; }
    }

    public class AuthorDto
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
    }
}
