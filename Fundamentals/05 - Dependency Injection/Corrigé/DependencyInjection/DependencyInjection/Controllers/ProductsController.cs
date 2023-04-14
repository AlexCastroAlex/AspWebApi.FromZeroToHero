using DependencyInjection.Models;
using DependencyInjection.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DependencyInjection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService= productService;
        }

        [HttpGet("getall")]
        public IActionResult GetAllProducts()
        {
            return Ok(_productService.GetProducts());
        }


        [HttpGet("getbyid/{id}")]
        public IActionResult GetAllProducts(int id)
        {
            return Ok(_productService.GetProductById(id));
        }

        [HttpPost]
        public IActionResult AddProduct([FromBody] Product product)
        {
            if(product is not null)
            {
                _productService.AddProduct(product);
                return Created($"api/Products/{product.Id}",product);
            }
            return BadRequest();
        }


        [HttpDelete("{id}")]
        public IActionResult RemoveProduct(int id)
        {
           
            var product = _productService.GetProductById(id);
            if(product is not null)
            {
                _productService.RemoveProduct(product);
                return Ok();
            }
            else
            {
                return NotFound();
            }
                
        }


    }
}
