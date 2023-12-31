using Proiect.Entities;
using Proiect.Models;

namespace Proiect.Repositories
{
    public interface IArticolRepository
    {
        public Task<ICollection<Articol>> GetArticoleAsync();
        public Task<Articol?> GetArticolAsync(string titlu);
        public Task<ICollection<Articol>?> GetArticolAutorAsync(string userName);
        public Task<List<ArticolUtilizatorDto>?> GetArticoleGrupateAsync();
        public Task<Articol?> PutArticolAsync(string? userName, string titlu, Articol articol);
        public Task<Articol?> PostArticolAsync(string userName, Articol articol);
        public Task<bool> DeleteArticolAsync(int id);
        public Task<bool> DeleteArticolUtilizatorAsync(string userName, Articol articol);
    }
}
