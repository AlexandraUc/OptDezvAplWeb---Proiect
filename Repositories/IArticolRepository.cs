using Proiect.Entities;
using Proiect.Models;

namespace Proiect.Repositories
{
    public interface IArticolRepository
    {
        public Task<ICollection<Articol>> GetArticoleAsync();
        public Task<Articol?> GetArticolAsync(int id);
        public Task<ICollection<Articol>?> GetArticolAutorAsync(string id);
        public Task<List<ArticolUtilizatorDto>?> GetArticoleGrupateAsync();
        public Task<Articol> PutArticolAsync(Articol articol);
        public Task PostArticolAsync(Articol articol);
        public Task DeleteArticolAsync(Articol articol);
    }
}
