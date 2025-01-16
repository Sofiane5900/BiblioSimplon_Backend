
using BiblioAPI.Data;
using Microsoft.EntityFrameworkCore;
using BiblioAPI.Models;
using BiblioAPI.Services.Interfaces;
namespace BiblioAPI.Services
{
    public class LivreServices : ILivreService
    {
        private readonly BiblioDbContext _context;

        public LivreServices(BiblioDbContext context)
        {
            _context = context;
        }

        public IEnumerable<LivreModel> GetAllLivres()
        {
            return _context.Livre.ToList();
        }

        public LivreModel GetLivreById(int id)
        {
            return _context.Livre.Find(id);
        }

        public void AddLivre(LivreModel livre)
        {
            _context.Livre.Add(livre);
            _context.SaveChanges();
        }

        public void UpdateLivre(int id, LivreModel livre)
        {
            var existingLivre = _context.Livre.Find(id);
            if (existingLivre != null)
            {
                existingLivre.Titre = livre.Titre;
                existingLivre.ISBN = livre.ISBN;
                _context.SaveChanges();
            }
        }

        public void DeleteLivre(int id)
        {
            var livre = _context.Livre.Find(id);
            if (livre != null)
            {
                _context.Livre.Remove(livre);
                _context.SaveChanges();
            }
        }
    }
}
