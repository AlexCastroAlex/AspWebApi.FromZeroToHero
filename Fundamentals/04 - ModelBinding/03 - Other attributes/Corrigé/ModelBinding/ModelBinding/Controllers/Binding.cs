using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelBinding.Models;

namespace ModelBinding.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Binding : ControllerBase
    {
        public Person person { get; set; }

        [HttpGet("querystringsimpletype")]
        public IActionResult Get([FromQuery]string name, [FromQuery] int age)
        {
            return Ok($"Name is {name} and age is {age}");
        }


        [HttpGet("querystringcomplexetype")]
        public IActionResult GetComplex([FromQuery] Person person)
        {
            return Ok($"Name is {person.Name} and age is {person.Age}");
        }

        [HttpGet("{name}/{age}")]
        public IActionResult FromRoute([FromRoute]string name, [FromRoute] int age)
        {
            return Ok($"Name is {name} and age is {age}");
        }

        [HttpGet("complextype/{Name}/{Age}")]
        public IActionResult FromRouteComplexType([FromRoute] Person person)
        {
            return Ok($"Name is {person.Name} and age is {person.Age}");
        }

        [HttpGet("complextypemixed/{Name}/{Age}")]
        public IActionResult FromRouteComplexTypeMixed([FromRoute] Person person, [FromQuery] int id)
        {
            return Ok($"Name is {person.Name} and age is {person.Age} with id {id}");
        }

        [HttpPost("postfrombody")]
        public IActionResult FromRoutePost([FromBody] Person person)
        {
            return Ok($"Name is {person.Name} and age is {person.Age}");
        }

        [HttpPost("postfrombodymixed")]
        public IActionResult FromRoutePostMixed([FromBody] Person person, [FromQuery] int id)
        {
            return Ok($"Name is {person.Name} and age is {person.Age} and [FromQuery] id is {id}");
        }

        [HttpPost("fromform")]
        public IActionResult FromForm([FromForm] Person person)
        {
            return Ok($"Name is {person.Name} and age is {person.Age}");
        }

        [HttpGet("fromheader")]
        public IActionResult FromHeader([FromHeader(Name ="user-agent")] string userAgent)
        {
            return Ok($"User agent is {userAgent}");
        }

        [HttpGet("fromheadermultiple")]
        public IActionResult FromHeadermultiple([FromHeader(Name = "user-agent")] string userAgent,
            [FromHeader(Name = "name")] string name,
            [FromHeader(Name = "age")] string age)
        {
            return Ok($"User agent is {userAgent}. Name is {name} and age is {age}.");
        }


    }
}
