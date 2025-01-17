using BiblioAPI.Data;
using BiblioAPI.Models;
using BiblioAPI.Services.Interfaces;
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

        public IEnumerable<GetLivreDTO> GetAllLivres()
        {
            // Mapping de Livre en GetLivreDTO
            return _context.Livre.Select(livre => new GetLivreDTO
            {
                Id = livre.Id,
                Titre = livre.Titre,
                Auteur = livre.Auteur,
                ISBN = livre.ISBN,
            });
        }

        public GetLivreDTO GetLivreById(int Id)
        {
            return _context
                .Livre.Select(livre => new GetLivreDTO
                {
                    Id = livre.Id,
                    Titre = livre.Titre,
                    Auteur = livre.Auteur,
                    ISBN = livre.ISBN,
                })
                .FirstOrDefault(livre => livre.Id == Id);
        }

        public void AddLivre(PostLivreDTO livre)
        {
            _context.Livre.Add(
                new LivreModel
                {
                    Titre = livre.Titre,
                    Auteur = livre.Auteur,
                    ISBN = livre.ISBN,
                }
            );
            _context.SaveChanges();
        }

        public void UpdateLivre(int Id, PostLivreDTO livre)
        {
            var existingLivre = _context.Livre.Find(Id);
            if (existingLivre != null)
            {
                existingLivre.Titre = livre.Titre;
                existingLivre.ISBN = livre.ISBN;
                _context.SaveChanges();
            }
        }

        public void DeleteLivre(int Id)
        {
            var livre = _context.Livre.Find(Id);
            if (livre != null)
            {
                _context.Livre.Remove(livre);
                _context.SaveChanges();
            }
        }
    }
}
