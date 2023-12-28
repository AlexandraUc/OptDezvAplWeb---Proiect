using Proiect.Entities;
using Microsoft.AspNetCore.Identity;

namespace Proiect.Services
{
    public class UtilizatorService: IUtilizatorService
    {
        private readonly UserManager<Utilizator> _userManager;
        public UtilizatorService(UserManager<Utilizator> userManager)
        {
            _userManager = userManager;
        }

        // Seteaza utilizatorId si Utilizator pentru un articol atunci cand e creat
        public async Task<Articol?> AdaugaArticol(string userName, Articol articol)
        {
            var utilizator = await _userManager.FindByNameAsync(userName);

            if(utilizator == null)
                return null;

            articol.UtilizatorId = utilizator.Id;
            articol.Utilizator = utilizator;

            return articol;
        }

        // Verifica daca utilizatorul curent are acces la articol
        public async Task<bool> VerificaArticol(string userName, Articol articol) {
            var utilizator = await _userManager.FindByNameAsync(userName);

            if(utilizator.Id == articol.UtilizatorId)
                return true;

            return false;
        }
        public async Task<Utilizator?> GetUtilizator(string? userName)
        {
            var utilizator = await _userManager.FindByNameAsync(userName);
            return utilizator;
        }

        // Seteaza profilul unui utilizator
        public Profil SeteazaUtilizatorProfil(Utilizator utilizator, Profil profil)
        {
            profil.UtilizatorId = utilizator.Id;
            profil.Utilizator = utilizator;

            return profil;
        }
    }
}
