
using Newtonsoft.Json;

namespace Bookstore.FrontEnd.Clients
{
    public class BookStoreClient
    {
        private readonly HttpClient _http;
        private readonly IConfiguration _configuration;
        public BookStoreClient(HttpClient http, IConfiguration configuration)
        {
            _http = http;
            _configuration = configuration;
        }
    }
}
