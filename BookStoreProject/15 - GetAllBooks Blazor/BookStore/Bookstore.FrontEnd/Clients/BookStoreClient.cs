
using Bookstore.FrontEnd.Configuration;
using Bookstore.FrontEnd.DTO;
using Newtonsoft.Json;

namespace Bookstore.FrontEnd.Clients
{
    public class BookStoreClient
    {
        private readonly HttpClient _http;
        private readonly IConfiguration _configuration;
        private readonly BookstoreApiSettings bookstoreConfig = new BookstoreApiSettings();
        public BookStoreClient(HttpClient http, IConfiguration configuration)
        {
            _http = http;
            _configuration = configuration;
            _configuration.GetSection("BookstoreApiSettings").Bind(bookstoreConfig);
        }

        public async Task<List<BookDto>> GetAllBooksAsync()
        {
            var result = await _http.GetAsync(bookstoreConfig.BooksPath);
            if(!result.IsSuccessStatusCode)
            {
                return new List<BookDto>();
            }
            var resultContent = await result.Content.ReadAsStringAsync();
            var finalResult = JsonConvert.DeserializeObject<List<BookDto>>(resultContent);
            return finalResult ?? new List<BookDto>();
        }

        public async Task<List<AuthorDto>> GetAllAuthors()
        {
            var result = await _http.GetAsync(bookstoreConfig.AuthorsPath);
            if (!result.IsSuccessStatusCode)
            {
                return new List<AuthorDto>();
            }
            var resultContent = await result.Content.ReadAsStringAsync();
            var finalResult = JsonConvert.DeserializeObject<List<AuthorDto>>(resultContent);
            return finalResult ?? new List<AuthorDto>();
        }

    }
}
