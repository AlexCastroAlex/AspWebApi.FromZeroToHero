using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        public List<Animal> animals = null;
        public AnimalsController()
        {
            animals = new List<Animal>
            {
                new Animal { Id = 1 ,Age = 1 , Name="Kitty" , Description="This is a cat"},
                new Animal { Id = 2 ,Age = 4 , Name="Snoopy" , Description="This is a dog"},
                new Animal { Id = 3 ,Age = 45 , Name="Woody Woodpecker" , Description="This is a woodpecker"},
            };
        }

        [HttpGet("onlystatus200")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult OnlyStatus200Ok()
        {
            return Ok();
        }


        [HttpGet("animals")]
        [ProducesResponseType(typeof(List<Animal>), StatusCodes.Status200OK)]
        public IActionResult GetAllAnimals()
        {
            return Ok(animals);
        }

        [HttpGet("{id}")]
        public IActionResult GetAnimalById([FromRoute] int id)
        {
            var animal = animals.Where(x => x.Id == id).FirstOrDefault();
            if(animal is null)
            {
                return NotFound();
            }
            return Ok(animal);
        }



        /// <summary>
        /// Ok Created
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("")]
        public IActionResult AddAnimal([FromBody] Animal model)
        {
            // code to add new employee here  
            // int id = call to service.  
            return Created("~api/animals/1", model);
        }

        /// <summary>
        /// Created at action
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("createdataction")]
        public IActionResult AddAnimalAtAction([FromBody] Animal model)
        {
            // code to add new employee here  
            int newEmployeeId = 1;
            return CreatedAtAction("GetAnimalById", new { id = newEmployeeId }, model);
        }



        /// <summary>
        /// Created at route
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("createdatRoute")]
        public IActionResult AddAnimalAtRoute([FromBody] Animal model)
        {

            if (string.IsNullOrEmpty(model.Description))
            {
                // returns 400 status code  
                return BadRequest();
            }


            // code to add new employee here    
            int newEmployeeId = 1; // get this id from database.    
            return CreatedAtRoute("getAnimalById", new { id = newEmployeeId }, model);
        }

    }
}
