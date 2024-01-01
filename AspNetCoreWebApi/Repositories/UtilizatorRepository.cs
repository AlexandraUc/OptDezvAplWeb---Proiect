using Proiect.Entities;
using Proiect.Models;
using Microsoft.EntityFrameworkCore;
using Proiect.ContextModels;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace Proiect.Repositories
{
    public class UtilizatorRepository: IUtilizatorRepository
    {
        private readonly ProiectContext _context;
        private readonly UserManager<Utilizator> _userManager;
        public UtilizatorRepository(ProiectContext context, UserManager<Utilizator> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<ICollection<Utilizator>> GetUtilizatoriAsync()
        {
            var utilizatori = await _userManager.Users.ToListAsync();
            return utilizatori;
        }
        public async Task<Utilizator?> GetUtilizatorAsync(string id)
        {
            var utilizator = await _userManager.FindByIdAsync(id);
            return utilizator;
        }
        public async Task<UtilizatorProfilDto?> GetUtilizatorProfilDtoAsync(string userName)
        {
            var utilizator = await _userManager.Users
                .Join(_context.Profil, u => u.Id, p => p.UtilizatorId, (u, p) => new UtilizatorProfilDto
                {
                    Nume = p.Nume,
                    UserName = u.UserName,
                    Prenume = p.Prenume,
                    Articole = p.Articole
                }).FirstOrDefaultAsync(u => u.UserName == userName);

            return utilizator;
        }
        public void DeleteUtilizator(Utilizator utilizator)
        {
            _context.Utilizator.Remove(utilizator);
        }
        public async Task<bool> DeleteUtilizatorAsync(string userName)
        {
            var utilizator = await _userManager.FindByNameAsync(userName);

            if(utilizator != null)
            {
                await _userManager.DeleteAsync(utilizator);
                return true;
            }

            return false;
        }
    }
}
