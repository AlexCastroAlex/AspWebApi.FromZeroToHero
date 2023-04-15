using BookStore.Api.DTO;
using FluentValidation;

namespace BookStore.Api.Extensions
{
    public static class AddValidatorsExtensions
    {
        public static IServiceCollection AddValidators(this IServiceCollection services) 
        {
            services.AddScoped<IValidator<BookDTO>, BookDTOValidator>();
            //ici mes autres validateurs

            return services;

        }
    }
}
