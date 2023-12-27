using Proiect.Entities;
using Proiect.Models;

namespace Proiect.Repositories
{
    public interface IUtilizatorRepository
    {
        public Task<ICollection<Utilizator>> GetUtilizatoriAsync();
        public Task<Utilizator?> GetUtilizatorAsync(string id);
        public Task<UtilizatorProfilDto?> GetUtilizatorProfilDtoAsync(string id);
        public Task<Utilizator> PutUtilizatorAsync(Utilizator utilizator);
        public Task PostUtilizatorAsync(Utilizator utilizator);
        public Task DeleteUtilizatorAsync(Utilizator utilizator);
    }
}
