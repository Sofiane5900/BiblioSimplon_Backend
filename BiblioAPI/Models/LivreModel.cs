using BiblioAPI.Models;
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
    }

    public class PostLivreDTO
    {
        public string Titre { get; set; }
        public string Auteur { get; set; }
        public string ISBN { get; set; }
    }

    public class GetLivreDTO
    {
        public int Id { get; set; }
        public string Titre { get; set; }
        public string Auteur { get; set; }
        public string ISBN { get; set; }
        public bool EstDisponible { get; set; } = true;
    }
}
