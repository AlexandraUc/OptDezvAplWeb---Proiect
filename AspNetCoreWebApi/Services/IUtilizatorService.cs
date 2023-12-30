using Proiect.Entities;

namespace Proiect.Services
{
    public interface IUtilizatorService
    {
        public Task<Articol?> AdaugaArticol(string userName, Articol articol);
        public Task<bool> VerificaArticol(string userName, Articol articol);
        public Profil SeteazaUtilizatorProfil(Utilizator utilizator, Profil profil);
        public Task<Utilizator?> GetUtilizator(string? userName);

    }
}
