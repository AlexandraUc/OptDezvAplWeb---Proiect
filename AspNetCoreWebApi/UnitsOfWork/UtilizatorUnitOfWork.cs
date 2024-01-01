using Proiect.ContextModels;
using Proiect.Entities;
using Proiect.Repositories;
using Proiect.Services;

namespace Proiect.UnitsOfWork
{
    public class UtilizatorUnitOfWork: IUtilizatorUnitOfWork
    {
        private readonly ProiectContext _context;
        public IArticolRepository ArticolRepository { get; }
        public IProfilRepository ProfilRepository { get; }
        public IUtilizatorRepository UtilizatorRepository { get; }
        public IUtilizatorService UtilizatorService { get; }
        public UtilizatorUnitOfWork(ProiectContext context, IArticolRepository articolRepository, IProfilRepository profilRepository,
               IUtilizatorRepository utilizatorRepository, IUtilizatorService utilizatorService)
        {
            _context = context;
            ArticolRepository = articolRepository;
            ProfilRepository = profilRepository;
            UtilizatorRepository = utilizatorRepository;
            UtilizatorService = utilizatorService;
        }
        public void BeginTransaction()
        {
            _context.Database.BeginTransaction();
        }
        public void Commit() {
            _context.SaveChanges();
            _context.Database.CommitTransaction();
        }
        public void Rollback() { 
            _context.Database.RollbackTransaction();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
        public async Task<Utilizator?> StergeProfil(string userName)
        {
            var utilizator = await UtilizatorService.GetUtilizator(userName);
            if (utilizator != null)
            {
                var utilizatorId = utilizator.Id;

                var profil = await ProfilRepository.GetProfilUtilizatorAsync(utilizatorId);

                if(profil != null)
                {
                    ProfilRepository.DeleteProfilFaraSave(profil);
                }
            }
            return utilizator;
        }
        public async Task StergeArticole(string userName)
        {
            var articole = await ArticolRepository.GetArticolAutorAsync(userName);

            if(articole != null)
            {
                foreach(var articol in articole)
                {
                    ArticolRepository.DeleteArticolUtilizator(articol);
                }
            }
        }
    }
}
