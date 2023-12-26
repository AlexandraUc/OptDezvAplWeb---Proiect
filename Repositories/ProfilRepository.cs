using Proiect.Entities;
using Proiect.ContextModels;
using Microsoft.EntityFrameworkCore;

namespace Proiect.Repositories
{
    public class ProfilRepository: IProfilRepository
    {
        private readonly ProiectContext _context;
        public ProfilRepository(ProiectContext context)
        {
            _context = context;
        }
        public async Task<ICollection<Profil>> GetProfiluriAsync()
        {
            return await _context.Profil.Include(p => p.Articole).ToListAsync();
        }
        public async Task<Profil?> GetProfilAsync(int id)
        {
            return await _context.Profil.Include(p => p.Articole).
                                        FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Profil> PutProfilAsync(Profil profil)
        {
            _context.Profil.Update(profil);
            await _context.SaveChangesAsync();
            return profil;
        }
        public async Task PostProfilAsync(Profil profil)
        {
            _context.Profil.Add(profil);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteProfilAsync(Profil profil)
        {
            _context.Profil.Remove(profil);
            await _context.SaveChangesAsync();
        }
    }
}
