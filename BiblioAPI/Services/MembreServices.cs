using BiblioAPI.Data;
using BiblioAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BiblioAPI.Services
{
    public class MembreService
    {
        private readonly BiblioDbContext _context;

        public MembreService(BiblioDbContext  context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MembreModel>> GetAllMembersAsync()
        {
            return await _context.Membre.Include(m => m.Emprunts).ToListAsync();
        }

        public async Task<MembreModel> GetMemberByIdAsync(int id)
        {
            return await _context.Membre
                .Include(m => m.Emprunts)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<MembreModel> AddMemberAsync(MembreModel membre)
        {
            _context.Membre.Add(membre);
            await _context.SaveChangesAsync();
            return membre;
        }

        public async Task<bool> UpdateMemberAsync(int id, MembreModel updatedMember)
        {
            if (id != updatedMember.Id)
            {
                return false;
            }

            _context.Entry(updatedMember).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemberExists(id))
                {
                    return false;
                }
                throw;
            }
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

        private bool MemberExists(int id)
        {
            return _context.Membre.Any(m => m.Id == id);
        }
    }

}
