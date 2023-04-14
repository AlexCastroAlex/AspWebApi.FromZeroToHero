using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [ApiController]
    public class TestController : ControllerBase
    {
        [Route("api/test-1")]
        public string Test1()
        {
            return "Hello from test!";

        }
        [Route("api/test-2")]
        public string Test2()
        {
            return "Hello from test 2!";

        }
        [Route("api/test-3")]
        public string Test3()
        {
            return "Hello from test 3!";

        }
    }
}
