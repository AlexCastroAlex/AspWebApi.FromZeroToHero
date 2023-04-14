using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using WebApplication1.Controllers;

namespace WebApplication1
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class MyFirstMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MyFirstMiddleware> _logger;

        public MyFirstMiddleware(RequestDelegate next , ILogger<MyFirstMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public Task Invoke(HttpContext httpContext)
        {
            Console.WriteLine("My first general middleware is writing in console!");
            _logger.LogError("My first general middleware is writing in console with logger!");
            return _next(httpContext);
        
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MyFirstMiddlewareExtensions
    {
        public static IApplicationBuilder UseMyFirstMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MyFirstMiddleware>();
        }
    }
}
