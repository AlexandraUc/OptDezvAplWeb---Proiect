using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Proiect.ContextModels;
using Proiect.Entities;
using Proiect.Models;
using Proiect.Repositories;

namespace Proiect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfiluriController : ControllerBase
    {
        private readonly ProiectContext _context;
        private readonly IProfilRepository _profilRepository;
        private readonly IMapper _mapper;
        public ProfiluriController(ProiectContext context, IProfilRepository profilRepository, IMapper mapper)
        {
            _context = context;
            _profilRepository = profilRepository;
            _mapper = mapper;
        }

        // Get
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetProfiluri()
        {
            if(_context.Profil == null)
                return NotFound();

            var profiluri = await _profilRepository.GetProfiluriAsync();

            if(profiluri == null)
                return NotFound();

            return Ok(profiluri);
        }

        // Get cu id
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetProfil(int id)
        {
            var profil = await _profilRepository.GetProfilAsync(id);

            if(profil == null)
                return NotFound();

            return Ok(profil);
        }

        // Put 
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> PutProfil(PostProfilDto profil)
        {
            var userName = User.Identity.Name;

            if (!await _profilRepository.PutProfilAsync(userName, _mapper.Map<Profil>(profil)))
                return Unauthorized();
            return Ok();
        }

        // Post
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostProfil(PostProfilDto profil)
        {
            var userName = User.Identity.Name;

            if (!await _profilRepository.PostProfilAsync(userName, _mapper.Map<Profil>(profil)))
                return Unauthorized();
            return Ok();
        }

        // Delete de admin (poate sterge orice profil)
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProfil(int id)
        {
            var profil = await _context.Profil.FindAsync(id);

            if(profil == null)
                return NotFound();

            await _profilRepository.DeleteProfilAsync(profil);
            return Ok();
        }

        // Delete de catre utilizator (isi poate sterge doar propriul profil)
        [HttpDelete("de-utilizator")]
        [Authorize]
        public async Task<IActionResult> DeleteProfilUtilizator()
        {
            var userName = User.Identity.Name;

            if (await _profilRepository.DeleteProfilUtilizatorAsync(userName))
                return Ok();

            return NotFound();
        }
    }
}
