using Proiect.Entities;
using Proiect.Models;
using Microsoft.EntityFrameworkCore;
using Proiect.ContextModels;
using AutoMapper;

namespace Proiect.Repositories
{
    public class UtilizatorRepository: IUtilizatorRepository
    {
        private readonly ProiectContext _context;
        private readonly IMapper _mapper;
        public UtilizatorRepository(ProiectContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;   
        }
        public async Task<ICollection<Utilizator>> GetUtilizatoriAsync()
        {
            var utilizatori = await _context.Utilizator.ToListAsync();
            return utilizatori;
        }
        public async Task<Utilizator?> GetUtilizatorAsync(string id)
        {
            var utilizator = await _context.Utilizator.FindAsync(id);
            return utilizator;
        }
        public async Task<UtilizatorProfilDto?> GetUtilizatorProfilDtoAsync(string id)
        {
            var utilizator = await _context.Utilizator
                .Join(_context.Profil, u => u.Id, p => p.UtilizatorId, (u, p) => new UtilizatorProfilDto
                {
                    Id = u.Id,
                    Nume = p.Nume,
                    Prenume = p.Prenume,
                    Email = u.Email
                }).FirstOrDefaultAsync(u => u.Id == id);

            return utilizator;
        }
        public async Task<Utilizator> PutUtilizatorAsync(Utilizator utilizator)
        {
            // Updateaza utilizatorul dupa ce stim ca exista
            _context.Utilizator.Update(utilizator);
            await _context.SaveChangesAsync();
            return utilizator;
        }
        public async Task PostUtilizatorAsync(Utilizator utilizator)
        {
            // Adauga utilizator
            _context.Utilizator.Add(utilizator);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteUtilizatorAsync(Utilizator utilizator)
        {
            _context.Utilizator.Remove(utilizator);
            await _context.SaveChangesAsync();
        }
    }
}
