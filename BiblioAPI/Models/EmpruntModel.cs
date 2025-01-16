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
        public DateTime DateEmprunt { get; set; }
        public DateTime DateRetour { get; set; }
    }
}
