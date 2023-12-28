using Microsoft.AspNetCore.Identity;
using Proiect.ContextModels;
using Proiect.Entities;
using Proiect.Repositories;
using Proiect.Services;

namespace Proiect.UnitsOfWork
{
    public class ArticolProfilUnitOfWork: IArticolProfilUnitOfWork
    {
        private readonly ProiectContext _context;
        private readonly UserManager<Utilizator> _userManager;
        public ArticolProfilUnitOfWork(ProiectContext context, UserManager<Utilizator> userManager)
        {
            _context = context;
            _userManager = userManager;

            UtilizatorService = new UtilizatorService(_userManager);
            ArticolRepository = new ArticolRepository(_context, UtilizatorService);
            ProfilRepository = new ProfilRepository(_context, UtilizatorService);
            
        }
        public IProfilRepository ProfilRepository { get; set; }
        public IArticolRepository ArticolRepository { get; set; }
        public IUtilizatorService UtilizatorService { get; set; }
        public void BeginTransaction()
        {
            _context.Database.BeginTransaction();
        }
        public void Commit()
        {
            _context.SaveChanges();
            _context.Database.CommitTransaction();
        }
        public void Rollback()
        {
            _context.Database.RollbackTransaction();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
        public bool AtribuieArticolProfil(Articol? articol, Profil? profil)
        {
            if(profil != null && articol != null)
            {
                if(profil.Articole == null)
                    profil.Articole = new List<Articol>();

                if(articol.Profiluri == null)
                    articol.Profiluri = new List<Profil>();

                profil.Articole.Add(articol);
                articol.Profiluri.Add(profil);

                return true;
            }
            return false;
        }
    }
}
