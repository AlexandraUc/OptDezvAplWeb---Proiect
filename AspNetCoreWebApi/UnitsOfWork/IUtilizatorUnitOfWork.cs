using Proiect.Entities;
using Proiect.Repositories;
using Proiect.Services;

namespace Proiect.UnitsOfWork
{
    public interface IUtilizatorUnitOfWork: IDisposable
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
        IArticolRepository ArticolRepository { get; }
        IProfilRepository ProfilRepository { get; }
        IUtilizatorRepository UtilizatorRepository { get; }
        IUtilizatorService UtilizatorService { get; }
        public Task<Utilizator?> StergeProfil(string userName);
        public Task StergeArticole(string userName);
    }
}
