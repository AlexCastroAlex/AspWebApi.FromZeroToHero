using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        [HttpGet("employees")]
        public List<Employee> GetAllEmployee()
        {
            return new List<Employee> {
                new Employee{ age = 30, firstName = "Alex", lastName = "Castro", id = 1},
                new Employee{ age = 20, firstName = "You", lastName = "Learner", id = 2}
            };
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
