using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace BiblioAPI.Models
{
    public class MembreModel
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public ICollection<EmpruntModel> Emprunts { get; set; }
    }

    public class GetMembreDTO
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
    }

    public class PostMembreDTO
    {
        [Required(ErrorMessage = "Veuillez saisir un Nom")]
        [MinLength(1, ErrorMessage = "Le Nom ne peut pas être vide.")]
        public string Nom { get; set; }
        [Required(ErrorMessage = "Veuillez saisir un prénom")]
        [MinLength(1, ErrorMessage = "Le prénom ne peut pas être vide.")]
        public string Prenom { get; set; }
        [Required(ErrorMessage = "Veuillez saisir une adresse e-mail.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
        ErrorMessage = "L'adresse e-mail n'est pas valide.")]
        [DefaultValue("exemple@domaine.com")]
        public string Email { get; set; }
    }
}
