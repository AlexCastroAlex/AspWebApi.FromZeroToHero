using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelBinding.CustomBinding;
using ModelBinding.Models;

namespace ModelBinding.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBinding : ControllerBase
    {
        [HttpGet("nationalities")]
        public IActionResult GetCountriesArray([ModelBinder(BinderType = typeof(CustomBinder))]string[] nationalities)
        {
            return Ok(nationalities);
        }

        [HttpGet("{id}")]
        public IActionResult GetPersonDetails([ModelBinder(Name ="Id")] PersonDetails person)
        {
            return Ok(person);
        }

    }
}
