using System.ComponentModel.DataAnnotations;

namespace Bookstore.FrontEnd.DTO
{
    public class BookCreationDTO
    {
        [Required(ErrorMessage = "Le champ Title est requis")]
        public string Title { get; set; } = string.Empty;
        [Required(ErrorMessage = "Le champ Description est requis")]
        public string Description { get; set; } = string.Empty;
        [Required(ErrorMessage = "Le champ Auteur est requis")]
        public int AuthorId { get; set; }
    }
}
