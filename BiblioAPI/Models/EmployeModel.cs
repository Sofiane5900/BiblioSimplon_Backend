using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BiblioAPI.Models
{
    public class EmployeModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
    public class GetEmployeDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        
    }

    public class RegisterEmployeDTO
    {
        // Utilisation d'un Required pour contraindre l'utilisateur à saisir un Nom d'utilisateur
        [Required(ErrorMessage = "Le nom d'utilisateur ne peut pas être vide. Veuillez saisir un nom d'utilsateur")] 
       
        public string Username { get; set; }
        
        // Utilisation d'un required pour la saisi de l'adresse mail si elle est vide 
        [Required(ErrorMessage = "Veuillez saisir une adresse e-mail.")]
        // Utilisation d'un RegularExpresion avec regex pour le controle de validité de l'adresse mail 
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
        ErrorMessage = "L'adresse e-mail n'est pas valide.")]
        // Utilisation de DefaultValue pour éviter les aléatoires de la regex, il sert également d'exemple
        [DefaultValue("exemple@domaine.com")]
        public string Email { get; set; }

        //Utilisation d'un required pour que la saisi ne soit pas vide 
        [Required(ErrorMessage = "Le mot de passe est obligatoire.")]
        // Utilisation du regularExpression avec regex pour contraindre l'utilisateur à saisir au moins 6 caractères, dont un chiffre
        [RegularExpression(@"^(?=.*\d)[a-zA-Z\d]{6,}$",
       ErrorMessage = "Le mot de passe doit contenir au moins 6 caractères, dont un chiffre.")]
        // Utilisation de DefaultValue pour éviter les aléatoires de la regex, il sert également d'exemple
        [DefaultValue("Admin123")]
        public string Password { get; set; }

        //Utilisation d'un required pour que la saisi ne soit pas vide 
        [Required(ErrorMessage = "Le rôle est obligatoire.")]
        // Utilisation du regularExpression avec regex pour contraindre l'utilisateur à saisir entre les deux roles ajout de (?i) pour qu'il soit pas sensible à la case
        [RegularExpression(@"^(?i)(Admin|Bibliothécaire)$",
        ErrorMessage = "Le rôle doit être soit 'Admin' soit 'Bibliothécaire'.")]
        // Utilisation de DefaultValue pour éviter les aléatoires de la regex, il sert également d'exemple
        [DefaultValue("Admin/Bibliothécaire")]
        public string Role { get; set; }
    }

    public class LoginEmployeDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class ResetPasswordDTO
    {
        [Required(ErrorMessage = "Utilisateur introuvable.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Le mot de passe est obligatoire.")]
        [RegularExpression(@"^(?=.*\d)[a-zA-Z\d]{6,}$",
       ErrorMessage = "Le mot de passe doit contenir au moins 6 caractères, dont un chiffre.")]
        [DefaultValue("Admin123")]
        public string NewPassword { get; set; }
    }
}
