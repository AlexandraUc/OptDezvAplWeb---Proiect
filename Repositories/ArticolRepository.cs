using Microsoft.EntityFrameworkCore;
using Proiect.ContextModels;
using Proiect.Entities;
using Proiect.Services;
using Proiect.Models;

namespace Proiect.Repositories
{
    public class ArticolRepository: IArticolRepository
    {
        private readonly ProiectContext _context;
        private readonly IUtilizatorService _utilizatorService;
        public ArticolRepository(ProiectContext context, IUtilizatorService utilizatorService)
        {
            _context = context;
            _utilizatorService = utilizatorService;
        }
        public async Task<ICollection<Articol>> GetArticoleAsync()
        {
            return (await _context.Articol.ToListAsync());
        }
        public async Task<Articol?> GetArticolAsync(string titlu)
        {
            var articol = await _context.Articol.Where(a => a.Titlu == titlu).ToListAsync(); 

            if(articol == null)
                return null;

            return articol[0];
        }
        public async Task<ICollection<Articol>?> GetArticolAutorAsync(string userName)
        {
            var articol = await _context.Articol.Where(x => x.Utilizator.UserName == userName).
                OrderBy(x => x.Titlu).ToListAsync();

            return articol;
        }
        public async Task<List<ArticolUtilizatorDto>?> GetArticoleGrupateAsync()
        {
            var articole = await _context.Articol
                        .GroupBy(a => new { a.UtilizatorId, a.Utilizator.UserName })
                        .OrderBy(a => a.Key.UtilizatorId)
                        .Select(grup => new ArticolUtilizatorDto  // Fiecare grup o sa fie reprezentat de un UtilizatorId si o                       
                        {                                    // lista de articole ordonate dupa titlu
                            UserName = grup.Key.UserName,
                            Articole = grup.OrderBy(a => a.Titlu).ToList()
                        })
                        .ToListAsync();

            return articole;
        }
        public async Task<Articol?> PutArticolAsync(string? userName, int id, Articol articol)
        {
            // Verifica daca exista articolul si daca este scris de utilizatorul care face request ul

            var ar = await _context.Articol.FindAsync(id);

            if (ar == null)
                return null;

            if (!await _utilizatorService.VerificaArticol(userName, ar))
                return null;

            ar.Titlu = articol.Titlu;
            ar.Continut = articol.Continut;

            _context.Articol.Update(ar);
            await _context.SaveChangesAsync();

            return ar;
        }
        public async Task<Articol?> PostArticolAsync(string userName, Articol articol)
        {
            var ar = await _utilizatorService.AdaugaArticol(userName, articol);
            
            if(ar == null)
                return null;

            _context.Articol.Add(ar);
            await _context.SaveChangesAsync();

            return ar;
        }
        public async Task<bool> DeleteArticolAsync(int id)
        {
            var articol = await _context.Articol.FindAsync(id);

            if (articol == null)
                return false;

            _context.Articol.Remove(articol);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> DeleteArticolUtilizatorAsync(string userName, Articol articol)
        {
            if(await _utilizatorService.VerificaArticol(userName, articol))
            {
                _context.Articol.Remove(articol);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }
    }
}
