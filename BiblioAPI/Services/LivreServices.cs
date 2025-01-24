using BiblioAPI.Data;
using BiblioAPI.Interfaces;
using BiblioAPI.Models;
using BiblioAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BiblioAPI.Services
{
    public class LivreServices : ILivreService
    {
        private readonly BiblioDbContext _context;

        public LivreServices(BiblioDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GetLivreDTO>> AfficherLivreAsync()
        {
            // Mapping de Livre en GetLivreDTO (asynchrone)
            return await _context
                .Livre.Select(livre => new GetLivreDTO
                {
                    Id = livre.Id,
                    Titre = livre.Titre,
                    Auteur = livre.Auteur,
                    ISBN = livre.ISBN,
                    EstDisponible = livre.EstDisponible,
                    ImageURL = livre.ImageURL,
                })
                .ToListAsync();
        }

        public async Task<GetLivreDTO> GetLivreByIdAsync(int id)
        {
            var livre = await _context.Livre.FindAsync(id);
            var LivrePostDTO = new GetLivreDTO
            {
                Id = livre.Id,
                Titre = livre.Titre,
                Auteur = livre.Auteur,
                ISBN = livre.ISBN,
                EstDisponible = livre.EstDisponible,
                ImageURL = livre.ImageURL,
            };
            return LivrePostDTO;
        }

        public async Task<PostLivreDTO>AddLivreAsync(PostLivreDTO livre)

        {

            if (await _context.Livre.AnyAsync(l => l.ISBN == livre.ISBN));
                
            var newLivre = new LivreModel
                {
                    Titre = livre.Titre,
                    Auteur = livre.Auteur,
                    ISBN = livre.ISBN,
                    ImageURL= livre.ImageURL,
                };
                _context.Livre.Add(newLivre);
                await _context.SaveChangesAsync();
                return livre;          
        }
        

        public async Task UpdateLivreAsync(int id, PostLivreDTO livre)
        {
            var existingLivre = await _context.Livre.FindAsync(id);
            if (existingLivre != null)
            {
                existingLivre.Titre = livre.Titre;
                existingLivre.Auteur = livre.Auteur;
                existingLivre.ISBN = livre.ISBN;
                existingLivre.ImageURL = livre.ImageURL;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteLivreAsync(int id)
        {
            var livre = await _context.Livre.FindAsync(id);
            if (livre != null)
            {
                _context.Livre.Remove(livre);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<GetLivreDTO>> GetLivresDisponiblesAsync()
        {

            return await _context.Livre
                .Where(livre => livre.EstDisponible == true)

                .Select(livre => new GetLivreDTO
                {
                    Id = livre.Id,
                    Titre = livre.Titre,
                    Auteur = livre.Auteur,
                    ISBN = livre.ISBN,
                    EstDisponible = livre.EstDisponible,
                    ImageURL = livre.ImageURL,
                })
                .ToListAsync();
        }

    }
}
