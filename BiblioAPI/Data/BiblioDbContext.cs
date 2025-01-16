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
    }
}
