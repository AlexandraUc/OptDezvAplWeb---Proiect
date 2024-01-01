using Proiect.Entities;

namespace Proiect.Repositories
{
    public interface IProfilRepository
    {
        public Task<ICollection<Profil>> GetProfiluriAsync();
        public Task<Profil?> GetProfilAsync(int id);
        public Task<Profil?> GetProfilUtilizatorAsync(string utilizatorId);
        public Task<bool> PutProfilAsync(string? userName, Profil profil);
        public Task<bool> PostProfilAsync(string? userName, Profil profil);
        public Task DeleteProfilAsync(Profil profil);
        public void DeleteProfilFaraSave(Profil profil);
        public Task<bool> DeleteProfilUtilizatorAsync(string userName);
    }
}
