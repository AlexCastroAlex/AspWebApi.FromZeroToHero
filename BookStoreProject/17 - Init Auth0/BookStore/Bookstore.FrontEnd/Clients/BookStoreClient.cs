
using Bookstore.FrontEnd.Configuration;
using Bookstore.FrontEnd.DTO;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;
using System.Text;

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

        public async Task<BookDto?> AddBook(BookCreationDTO bookCreationDTO)
        {

            var content = new StringContent(JsonConvert.SerializeObject(bookCreationDTO),
            Encoding.UTF8,
            Application.Json);
            var result = await _http.PostAsync(bookstoreConfig.BooksPath, content);
            if (!result.IsSuccessStatusCode)
            {
                return null;
            }
            var resultContent = await result.Content.ReadAsStringAsync();
            var finalResult = JsonConvert.DeserializeObject<BookDto>(resultContent);
            return finalResult ?? null;
        }

        public async Task<BookDto?> EditBook(BookDto bookDTO)
        {

            var content = new StringContent(JsonConvert.SerializeObject(new
            {
                Title = bookDTO?.Title,
                Description = bookDTO?.Description,
                AuthorId = bookDTO?.Author?.Id
            }),
            Encoding.UTF8,
            Application.Json);
            var result = await _http.PutAsync($"{bookstoreConfig.BooksPath}/{bookDTO?.Id}", content);
            if (!result.IsSuccessStatusCode)
            {
                return null;
            }
            var resultContent = await result.Content.ReadAsStringAsync();
            var finalResult = JsonConvert.DeserializeObject<BookDto>(resultContent);
            return finalResult ?? null;
        }


        public async Task<bool> DeleteBook(int id)
        {
            var result = await _http.DeleteAsync($"{bookstoreConfig.BooksPath}/{id}");
            if(result.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
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

        public async Task<AuthorDto?> AddAuthor(AuthorCreationDTO authorCreationDTO)
        {
       
            var content = new StringContent(JsonConvert.SerializeObject(authorCreationDTO),
            Encoding.UTF8,
            Application.Json);
            var result = await _http.PostAsync(bookstoreConfig.AuthorsPath, content);
            if (!result.IsSuccessStatusCode)
            {
                return null;
            }
            var resultContent = await result.Content.ReadAsStringAsync();
            var finalResult = JsonConvert.DeserializeObject<AuthorDto>(resultContent);
            return finalResult ?? null;
        }

        public async Task<AuthorDto?> EditAuthor(AuthorDto authorDto)
        {

            var content = new StringContent(JsonConvert.SerializeObject(new
            {
                authorDto.FirstName,
                authorDto.LastName
            }),
            Encoding.UTF8,
            Application.Json);
            var result = await _http.PutAsync($"{bookstoreConfig.AuthorsPath}/{authorDto.Id}", content);
            if (!result.IsSuccessStatusCode)
            {
                return null;
            }
            var resultContent = await result.Content.ReadAsStringAsync();
            var finalResult = JsonConvert.DeserializeObject<AuthorDto>(resultContent);
            return finalResult ?? null;
        }

        public async Task<bool> DeleteAuthor(int id)
        {
            var result = await _http.DeleteAsync($"{bookstoreConfig.AuthorsPath}/{id}");
            if (result.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

    }
}
