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
            modelBuilder.Entity<MembreModel>().ToTable("Membre");
            modelBuilder.Entity<LivreModel>().ToTable("Livre");
            modelBuilder.Entity<EmpruntModel>().ToTable("Emprunt");

            modelBuilder.Entity<MembreModel>().HasKey(m => m.Id);
            modelBuilder.Entity<LivreModel>().HasKey(l => l.Id);
            modelBuilder.Entity<EmpruntModel>().HasKey(e => e.Id);

            // Un membre peut emprunter 0 ou plusieurs livres mais un livre doit étre emprunté une seul fois
            modelBuilder
                .Entity<EmpruntModel>()
                .HasOne(e => e.Membre) // Un emprunt est associé à un seul membre
                .WithMany(m => m.Emprunts) // Un membre est associé à plusieurs emprunts
                .HasForeignKey(e => e.MembreId);
        }
    }
}
