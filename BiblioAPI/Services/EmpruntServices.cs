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
        public async Task<GetEmpruntDTO?> AfficherEmpruntId(int Id) // Opérateur ? nullable, emprunt peut étre null (on évite les crash)
        {
            var emprunt = await _context.Emprunt.FindAsync(Id);
            // Si emprunt est null (Id inéxistant) alors la méthode renvoie null (la response HTTP est géré par notre controller)
            if (emprunt is null)
            {
                return null;
            }
            // Mapping Emprunt en GetEmpruntDTO
            var empruntGetDTO = new GetEmpruntDTO
            {
                Id = emprunt.Id,
                MembreId = emprunt.MembreId,
                LivreId = emprunt.LivreId,
            };

            return empruntGetDTO;
        }

        // Méthode pour emprunter un livre
        public async Task<PostEmpruntDTO?> AjouterEmprunt(int membreId, int livreId)
        {
            // Vérifier si le livre existe
            var livre = await _context.Livre.FindAsync(livreId);
            if (livre == null)
            {
                return null;
            }

            // Vérifier si le membre existe
            var membre = await _context.Membre.FindAsync(membreId);
            if (membre == null)
            {
                return null;
            }

            // TODO : Vérifier si le livre est disponible (EstDisponible = false)
            if (livre.EstDisponible is false)
            {
                return null;
            }

            // Créer l'emprunt
            var emprunt = new EmpruntModel
            {
                MembreId = membreId,
                LivreId = livreId,
                DateEmprunt = DateTime.Now,
                DateRetour = DateTime.Now.AddDays(15), // Par exemple, 15 jours pour retourner le livre
            };

            // Ajouter l'emprunt à la base de données
            livre.EstDisponible = false;
            _context.Emprunt.Add(emprunt);

            // Mettre à jour la disponibilité du livre
            await _context.SaveChangesAsync();

            // Retourner l'objet DTO correspondant à l'emprunt créé
            var empruntDTO = new PostEmpruntDTO
            {
                MembreId = emprunt.MembreId,
                LivreId = emprunt.LivreId,
            };

            return empruntDTO;
        }

        // Méthode rendre mon livre emprunté delete = rendre
        public void RendreLivre(int Id)
        {
            var emprunt = _context.Emprunt.Find(Id);
            if (emprunt is not null)
            {
                var livre = _context.Livre.Find(emprunt.LivreId);
                livre.EstDisponible = true;
                _context.Emprunt.Remove(emprunt);
                _context.SaveChanges();
            }

            // Méthode pour modifier un emprunt
        }
    }
}
