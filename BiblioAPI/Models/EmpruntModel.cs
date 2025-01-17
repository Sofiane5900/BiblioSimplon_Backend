using BiblioAPI.Models;

namespace BiblioAPI.Models
{
    public class EmpruntModel
    {
        public int Id { get; set; }
        public int MembreId { get; set; }
        public MembreModel Membre { get; set; }
        public int LivreId { get; set; }
        public LivreModel Livre { get; set; }
        public DateTime DateEmprunt { get; set; } = DateTime.Now;
        public DateTime DateRetour { get; set; } = DateTime.Now.AddDays(15);
    }

    // DTO pour POST Emprunt
    public class PostEmpruntDTO()
    {
        public int MembreId { get; set; }
        public int LivreId { get; set; }
    }

    // DTO pour GET Emprunt
    public class GetEmpruntDTO()
    {
        public int Id { get; set; }
        public int MembreId { get; set; }
        public int LivreId { get; set; }
        public DateTime DateEmprunt { get; } = DateTime.Now;
        public DateTime DateRetour { get; } = DateTime.Now.AddDays(15);
    }
}
