using Microsoft.EntityFrameworkCore;
using Proiect.ContextModels;
using Proiect.Entities;
using Proiect.Models;
using Microsoft.VisualBasic;

namespace Proiect.Repositories
{
    public class ArticolRepository: IArticolRepository
    {
        private readonly ProiectContext _context;
        public ArticolRepository(ProiectContext context)
        {
            _context = context;
        }
        public async Task<ICollection<Articol>> GetArticoleAsync()
        {
            return (await _context.Articol.ToListAsync());
        }
        public async Task<Articol?> GetArticolAsync(int id)
        {
            var articol = await _context.Articol.FindAsync(id);
            return articol;
        }
        public async Task<ICollection<Articol>?> GetArticolAutorAsync(int id)
        {
            var articol = await _context.Articol.Where(x => x.UtilizatorId == id).
                OrderBy(x => x.Titlu).ToListAsync();
            return articol;
        }
        public async Task<List<ArticolUtilizatorDto>?> GetArticoleGrupateAsync()
        {
            var articole = await _context.Articol
                        .GroupBy(a => a.UtilizatorId)
                        .OrderBy(a => a.Key)
                        .Select(grup => new ArticolUtilizatorDto  // Fiecare grup o sa fie reprezentat de un UtilizatorId si o                       
                        {                                    // lista de articole ordonate dupa titlu
                            UtilizatorId = grup.Key,
                            Articole = grup.OrderBy(a => a.Titlu).ToList()
                        })
                        .ToListAsync();

            return articole;
        }
        public async Task<Articol> PutArticolAsync(Articol articol)
        {
            _context.Articol.Update(articol);
            await _context.SaveChangesAsync();
            return articol;
        }
        public async Task PostArticolAsync(Articol articol)
        {
            _context.Articol.Add(articol);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteArticolAsync(Articol articol)
        {
            _context.Articol.Remove(articol);
            await _context.SaveChangesAsync();
        }
    }
}
