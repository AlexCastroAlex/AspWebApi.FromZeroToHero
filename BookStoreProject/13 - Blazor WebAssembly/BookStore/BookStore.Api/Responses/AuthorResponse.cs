namespace BookStore.Api.Responses
{
    public class AuthorResponse
    {
        public int Id { get; set; }
        public required string FirstName { get; init; }
        public required string LastName { get; init; }
    }
}
