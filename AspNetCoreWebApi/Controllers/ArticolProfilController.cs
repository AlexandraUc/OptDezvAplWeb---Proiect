#pragma warning disable CS8602

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proiect.ContextModels;
using Proiect.Entities;
using Proiect.Repositories;
using Proiect.Services;

namespace Proiect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticolProfilController: ControllerBase
    {
        private readonly ProiectContext _context;
        private readonly IArticolRepository _articolRepository;
        private readonly IProfilRepository _profilRepository;
        private readonly IUtilizatorService _utilizatorService;
        public ArticolProfilController(ProiectContext context, IArticolRepository articolRepository, IProfilRepository profilRepository, IUtilizatorService utilizatorService)
        {
            _context = context;
            _articolRepository = articolRepository;
            _profilRepository = profilRepository;
            _utilizatorService = utilizatorService;
        }

        [HttpPut("titlu/{titlu}")]
        [Authorize]
        public async Task<IActionResult> PutArticolProfil(string titlu)
        {
            var userName = User.Identity.Name;

            var utilizator = await _utilizatorService.GetUtilizator(userName);
            var profil = await _profilRepository.GetProfilUtilizatorAsync(utilizator.Id);
            var articol = await _articolRepository.GetArticolAsync(titlu);

            if (profil != null && articol != null)
            {
                if (profil.Articole == null)
                    profil.Articole = new List<Articol>();

                if (articol.Profiluri == null)
                    articol.Profiluri = new List<Profil>();

                profil.Articole.Add(articol);
                articol.Profiluri.Add(profil);

                await _context.SaveChangesAsync();

                return Ok();
            }
            return BadRequest();
        }
    }
}
