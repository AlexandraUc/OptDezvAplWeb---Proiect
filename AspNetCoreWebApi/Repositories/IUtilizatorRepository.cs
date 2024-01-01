using Proiect.Entities;
using Proiect.Models;

namespace Proiect.Repositories
{
    public interface IUtilizatorRepository
    {
        public Task<ICollection<Utilizator>> GetUtilizatoriAsync();
        public Task<Utilizator?> GetUtilizatorAsync(string id);
        public Task<UtilizatorProfilDto?> GetUtilizatorProfilDtoAsync(string userName);
        public void DeleteUtilizator(Utilizator utilizator);
        public Task<bool> DeleteUtilizatorAsync(string userName);
    }
}
