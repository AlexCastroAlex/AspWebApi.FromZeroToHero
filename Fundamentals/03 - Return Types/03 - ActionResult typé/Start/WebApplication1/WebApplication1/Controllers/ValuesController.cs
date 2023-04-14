using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet("getvalue/{id:int:range(1,100)}")]
        public int GetValue(int id)
        {
            return id;
        }

        [HttpPost("postvalue/{id:minlength(1):maxlength(10)}")]
        public string PostValue(string id)
        {
            return id;
        }

        [HttpGet("employee/{id}")]
        public Employee GetEmployee(int id)
        {
            return new Employee
            {
                age = 30,
                firstName = "Alex",
                lastName = "Castro",
                id = 1   
            };
        }

    }
}
