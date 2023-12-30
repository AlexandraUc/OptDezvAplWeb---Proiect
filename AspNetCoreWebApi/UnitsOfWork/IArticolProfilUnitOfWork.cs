using Proiect.Entities;
using Proiect.Repositories;
using Proiect.Services;

namespace Proiect.UnitsOfWork
{
    public interface IArticolProfilUnitOfWork: IDisposable
    {
        void BeginTransaction();
        void Commit();
        void Rollback();   
        IArticolRepository ArticolRepository { get; }
        IProfilRepository ProfilRepository { get; }
        IUtilizatorService UtilizatorService { get; }
        public bool AtribuieArticolProfil(Articol? articol, Profil? profil);
    }
}
