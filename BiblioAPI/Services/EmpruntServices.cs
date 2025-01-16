using BiblioAPI.Data;
using BiblioAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BiblioAPI.Services
{
    public class EmpruntServices
    {
        // Réference a notre DbContext (car on veux accomplir des opérations sur la base de données)
        private readonly BiblioDbContext _context;

        // Constructeur, on injecte notre DbContext dans notre service via le constructeur
        public EmpruntServices(BiblioDbContext context)
        {
            _context = context; // On assigne le "context injecté" à notre "context local"
        }

        public async Task<bool> EmprunterLivre(int membreId, int livreId, DateTime dateRetour)
        {
            // On verifie si le livre existe
            var livre = await _context.Livre.FindAsync(livreId);
            if (livre is null)
            {
                return false;
            }

            // On verifie si le membre existe
            var membre = await _context.Membre.FindAsync(membreId);
            if (membre is null)
            {
                return false;
            }

            // On verifie si le livre est disponible
            if (livre.Emprunts.Any(e => e.DateRetour > DateTime.Now)) // (S'il y a une date de retour dans le futur)
            {
                return false;
            }

            var nouvelEmprunt = new EmpruntModel
            {
                MembreId = membreId,
                LivreId = livreId,
                DateEmprunt = DateTime.Now,
                DateRetour = dateRetour,
            };

            _context.Emprunt.Add(nouvelEmprunt);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
