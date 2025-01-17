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
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
    }
}
