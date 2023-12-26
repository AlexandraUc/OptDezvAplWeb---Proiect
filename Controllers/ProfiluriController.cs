using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proiect.ContextModels;
using Proiect.Entities;
using Proiect.Repositories;

namespace Proiect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfiluriController : ControllerBase
    {
        private readonly ProiectContext _context;
        private readonly IProfilRepository _profilRepository;
        public ProfiluriController(ProiectContext context, IProfilRepository profilRepository)
        {
            _context = context;
            _profilRepository = profilRepository;
        }

        // Get
        [HttpGet]
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
        public async Task<IActionResult> GetProfil(int id)
        {
            var profil = await _profilRepository.GetProfilAsync(id);

            if(profil == null)
                return NotFound();

            return Ok(profil);
        }

        // Put
        [HttpPut]
        public async Task<IActionResult> PutProfil(int  id, Profil profil)
        {
            if(id != profil.Id)
                return BadRequest();

            if(!ModelState.IsValid)
                return BadRequest();

            var pr = await _context.Profil.FindAsync(id);

            if(pr == null)
                return NotFound();

            await _profilRepository.PutProfilAsync(profil);
            return Ok(profil);
        }

        // Post
        [HttpPost]
        public async Task<IActionResult> PostProfil(Profil profil)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            await _profilRepository.PostProfilAsync(profil);
            return NoContent();
        }

        // Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfil(int id)
        {
            var profil = await _context.Profil.FindAsync(id);

            if(profil == null)
                return NotFound();

            await _profilRepository.DeleteProfilAsync(profil);
            return NoContent();
        }
    }
}
