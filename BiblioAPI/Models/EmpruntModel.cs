using BiblioAPI.Models;

namespace BiblioAPI.Models
{
    public class EmpruntModel
    {
        public int Id { get; set; }

        public int MembreId { get; set; }
        public MembreModel Membre { get; set; }
        public ICollection<LivreModel> Livres { get; set; }
        public DateTime DateEmprunt { get; set; }
        public DateTime DateRetour { get; set; }
    }
}
