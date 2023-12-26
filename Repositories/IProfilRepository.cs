using Proiect.Entities;

namespace Proiect.Repositories
{
    public interface IProfilRepository
    {
        public Task<ICollection<Profil>> GetProfiluriAsync();
        public Task<Profil?> GetProfilAsync(int id);
        public Task<Profil> PutProfilAsync(Profil profil);
        public Task PostProfilAsync(Profil profil);
        public Task DeleteProfilAsync(Profil profil);
    }
}
