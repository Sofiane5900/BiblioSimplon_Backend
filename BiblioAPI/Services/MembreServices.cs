using BiblioAPI.Data;
using BiblioAPI.Interfaces;
using BiblioAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BiblioAPI.Services
{
    public class MembreService : IMembreService
    {
        private readonly BiblioDbContext _context;

        public MembreService(BiblioDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GetMembreDTO>> GetAllMembersAsync()
        {
            return await _context
                .Membre.Select(membre => new GetMembreDTO
                {
                    Id = membre.Id,
                    Prenom = membre.Prenom,
                    Nom = membre.Nom,
                    Email = membre.Email,
                })
                .ToListAsync();
        }

        public async Task<PostMembreDTO> GetMemberByIdAsync(int id)
        {
            var member = await _context.Membre.FindAsync(id);
            var memberPostDTO = new PostMembreDTO
            {
                Prenom = member.Prenom,
                Nom = member.Nom,
                Email = member.Email,
            };
            return memberPostDTO;
        }

        public async Task<PostMembreDTO> AddMemberAsync(PostMembreDTO membre)
        {
            // mapping
            var newMember = new MembreModel
            {
                Prenom = membre.Prenom,
                Nom = membre.Nom,
                Email = membre.Email,
            };
            _context.Membre.Add(newMember);
            await _context.SaveChangesAsync();
            return membre;
        }

        public async Task<PostMembreDTO> UpdateMemberAsync(int Id, PostMembreDTO membre)
        {
            var existingMembre = await _context.Membre.FindAsync(Id);
            if (existingMembre != null)
            {
                existingMembre.Prenom = membre.Prenom;
                existingMembre.Nom = membre.Nom;
                existingMembre.Email = membre.Email;
                await _context.SaveChangesAsync();
            }
            return membre;
        }

        public async Task<bool> DeleteMemberAsync(int id)
        {
            var member = await _context.Membre.FindAsync(id);
            if (member == null)
            {
                return false;
            }

            _context.Membre.Remove(member);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
