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
        [ProducesResponseType(typeof(List<Employee>), StatusCodes.Status200OK)]
        public IActionResult GetAllEmployee()
        {
            return Ok(new List<Employee> {
                new Employee{ age = 30, firstName = "Alex", lastName = "Castro", id = 1},
                new Employee{ age = 20, firstName = "You", lastName = "Learner", id = 2}
            });
        }

        [HttpGet("employee/{id}")]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetEmployee(int id)
        {
            if(id == 0)
            {
                return NotFound();
            }

                return Ok( new Employee
            {
                age = 30,
                firstName = "Alex",
                lastName = "Castro",
                id = 1
            });
        }
    }
}
