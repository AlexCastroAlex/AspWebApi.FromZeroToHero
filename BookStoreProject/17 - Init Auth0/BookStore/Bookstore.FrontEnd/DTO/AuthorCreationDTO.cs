using System.ComponentModel.DataAnnotations;

namespace Bookstore.FrontEnd.DTO
{
    public class AuthorCreationDTO
    {
        [Required(ErrorMessage ="Le champ FirstName est requis")]
        public string FirstName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Le champ LastName est requis")]
        public string LastName { get; set; } = string.Empty;
    }
}
