using System.ComponentModel.DataAnnotations;
using BiblioAPI.Models;
using CommandLine.Text;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
namespace BiblioAPI.Models
{
    public class LivreModel
    {
        public int Id { get; set; }
       
        public string Titre { get; set; }
        
        public string Auteur { get; set; }
  
        public string ISBN { get; set; }
        public ICollection<EmpruntModel> Emprunts { get; set; }
        public bool EstDisponible { get; set; } = true;
        public string ImageURL { get; set; }
    }
    
    public class PostLivreDTO
    {
        [Required(ErrorMessage = "Le titre ne peut pas être vide. Veuillez saisir un titre")]
        public string Titre { get; set; }

        [Required(ErrorMessage = "L'auteur ne peut pas être vide. Veuillez saisir un auteur")]
        public string Auteur { get; set; }

        [Required(ErrorMessage = "L'ISBN est obligatoire.")]
        [RegularExpression(@"^(?:\d{9}X|\d{10}|\d{13}|\d{3}-\d{1,5}-\d{1,7}-\d{1,7}-\d{1})$", 
            ErrorMessage = "L'ISBN doit être au format ISBN-10 ou ISBN-13, avec ou sans tirets.")]
        [System.ComponentModel.DefaultValue("978-3-16-148410-0")]
        public string ISBN { get; set; }
        [Url(ErrorMessage = "L'URL de l'image n'est pas valide.")]
        public string ImageURL { get; set; }
    }

    public class GetLivreDTO
    {
        public int Id { get; set; }
        public string Titre { get; set; }
        public string Auteur { get; set; }
        public string ISBN { get; set; }
        public bool EstDisponible { get; set; } = true;
        public string ImageURL { get; set; }
    }
}
