﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelBinding.Models;

namespace ModelBinding.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [BindProperties]
    public class Binding : ControllerBase
    {
        public Person person { get; set; }


        [HttpPost("postroute")]
        public IActionResult Process()
        {
            return Ok($"Name is {this.person.Name} and age is {this.person.Age}");
        }

        [HttpGet("getroute")]
        public IActionResult ProcessGet()
        {
            return Ok($"Name is {this.person.Name} and age is {this.person.Age}");
        }
    }
}
