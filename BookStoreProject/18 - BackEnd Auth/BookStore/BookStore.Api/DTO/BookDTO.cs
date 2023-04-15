using BookStore.Repository.Models;
using FluentValidation;

namespace BookStore.Api.DTO
{
    public class BookDTO
    {
        public required string Title { get; init; }

        public required string Description { get; init; }

        public int AuthorId { get; set; }    
    }


    public class BookDTOValidator : AbstractValidator<BookDTO>
    {
        public BookDTOValidator()
        {
            RuleFor(book => book.Title).NotEmpty().WithMessage("Le titre ne peut être vide !")
                .MaximumLength(50).WithMessage("Le titre ne peut excéder 50 caractères!");
            RuleFor(book => book.Description).NotEmpty().MaximumLength(100);
        }
    }
}
