using System.Security.Cryptography.X509Certificates;
using BiblioAPI.Data;
using BiblioAPI.Interfaces;
using BiblioAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BiblioAPI.Services
{
    public class EmpruntServices : IEmpruntService
    {
        // Réference a notre DbContext (car on veux accomplir des opérations sur la base de données)
        private readonly BiblioDbContext _context;

        // Constructeur, on injecte notre DbContext dans notre service via le constructeur
        public EmpruntServices(BiblioDbContext context)
        {
            _context = context; // On assigne le "context injecté" à notre "context local"
        }

        // Méthode pour récuperer tout mes emprunts
        public async Task<List<GetEmpruntDTO>> AfficherEmprunts()
        {
            return await _context
                .Emprunt.Select(emprunt => new GetEmpruntDTO
                {
                    Id = emprunt.Id,
                    MembreId = emprunt.MembreId,
                    LivreId = emprunt.LivreId,
                })
                .ToListAsync();
        }

        // Méthode pour récuperer un emprunt par son Id
        public async Task<GetEmpruntDTO?> AfficherEmpruntId(int Id) // Opérateur ? nullable, au cas ou emprunt est null
        {
            var emprunt = await _context.Emprunt.FindAsync(Id);

            // Mapping Emprunt en EmpruntModelReadDTO
            var empruntGetDTO = new GetEmpruntDTO
            {
                Id = emprunt.Id,
                MembreId = emprunt.MembreId,
                LivreId = emprunt.LivreId,
            };

            return empruntGetDTO;
        }

        // Méthode pour emprunter un livre
        public async Task<bool> EmprunterLivre(int membreId, int livreId)
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
            //if (livre.Emprunts.Any(e => e.DateRetour > DateTime.Now))
            //{
            //    return false;
            //}

            var nouvelEmprunt = new EmpruntModel
            {
                MembreId = membreId,
                LivreId = livreId,
                DateEmprunt = DateTime.Now,
                DateRetour = DateTime.Now.AddDays(15),
            };

            _context.Emprunt.Add(nouvelEmprunt);
            await _context.SaveChangesAsync();
            return true;
        }

        // Méthode pour modifier un emprunt
        public async Task<bool> ModifierEmprunt(int Id, PostEmpruntDTO emprunt)
        {
            var empruntAModifier = await _context.Emprunt.FindAsync(Id);
            if (empruntAModifier is null)
            {
                return false;
            }

            empruntAModifier.MembreId = emprunt.MembreId;
            empruntAModifier.LivreId = emprunt.LivreId;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

            // Méthode pour modifier un emprunt 
            
        }
    }
}
