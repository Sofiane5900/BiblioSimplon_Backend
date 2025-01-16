using BiblioAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BiblioAPI.Data
{
    public class BiblioDbContext : DbContext
    {
        public BiblioDbContext(DbContextOptions<BiblioDbContext> options)
            : base(options) { }

        public DbSet<Models.MembreModel> Membre { get; set; }
        public DbSet<Models.LivreModel> Livre { get; set; }
        public DbSet<Models.EmpruntModel> Emprunt { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ** Création des tables **
            modelBuilder.Entity<MembreModel>().ToTable("Membre");
            modelBuilder.Entity<LivreModel>().ToTable("Livre");
            modelBuilder.Entity<EmpruntModel>().ToTable("Emprunt");

            modelBuilder.Entity<MembreModel>().HasKey(m => m.Id);
            modelBuilder.Entity<LivreModel>().HasKey(l => l.Id);
            modelBuilder.Entity<EmpruntModel>().HasKey(e => e.Id);

            // ** Association entre les tables **

            // Un membre peut emprunter 0 ou plusieurs livres mais un livre doit étre emprunté une seul fois
            modelBuilder
                .Entity<EmpruntModel>()
                .HasOne(e => e.Membre) // Un emprunt est associé à un seul membre
                .WithMany(m => m.Emprunts) // Un membre est associé à plusieurs emprunts
                .HasForeignKey(e => e.MembreId);

            // Un livre peut étre emprunté 0 ou plusieurs fois mais ne peut étre emprunter que par un seul membre
            modelBuilder
                .Entity<EmpruntModel>()
                .HasOne(e => e.Livre) // Un emprunt est associé à un seul livre
                .WithMany(l => l.Emprunts) // Un livre est associé à plusieurs emprunts
                .HasForeignKey(e => e.LivreId);

            // ** Contraintes Membre ** //

            // Nom, Prenom et Email sont obligatoires et ont une longueur maximale de 50 caractères
            modelBuilder
                .Entity<MembreModel>()
                .Property(m => m.Nom)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder
                .Entity<MembreModel>()
                .Property(m => m.Prenom)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder
                .Entity<MembreModel>()
                .Property(m => m.Prenom)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<MembreModel>().Property(m => m.Email).IsRequired().HasMaxLength(50);

            // ** Contraintes Livre ** //
            modelBuilder.Entity<LivreModel>().Property(l => l.Titre).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<LivreModel>().Property(l => l.ISBN).IsRequired().HasMaxLength(50);

            // ** Contraintes Emprunts ** //
            modelBuilder.Entity<EmpruntModel>().Property(e => e.DateEmprunt).IsRequired();
            modelBuilder.Entity<EmpruntModel>().Property(e => e.DateRetour).IsRequired();
        }
    }
}
