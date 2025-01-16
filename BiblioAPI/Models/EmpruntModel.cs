namespace BiblioAPI.Models
{
    public class EmpruntModel
    {
        public int Id { get; set; }

        //public Membre Membre { get; set; }
        //public ICollection<Livre> Livres { get; set; }
        public DateTime DateEmprunt { get; set; }
        public DateTime DateRetour { get; set; }
    }
}
