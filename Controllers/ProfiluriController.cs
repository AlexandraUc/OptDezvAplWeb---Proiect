using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proiect.ContextModels;
using Proiect.Entities;

namespace Proiect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfiluriController : ControllerBase
    {
        private readonly ProiectContext _context;
        public ProfiluriController(ProiectContext context)
        {
            _context = context;
        }

        // Get
        [HttpGet]
        public async Task<IActionResult> GetProfiluri()
        {
            if(_context.Profil == null)
                return NotFound();

            return Ok(await _context.Profil.Include(p => p.Articole).ToListAsync());
        }

        // Get cu id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfil(int id)
        {
            var profil = await _context.Profil.Include(p => p.Articole).
                                        FirstOrDefaultAsync(x => x.Id == id);

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

            _context.Profil.Update(profil);
            await _context.SaveChangesAsync();

            return Ok(profil);
        }

        // Post
        [HttpPost]
        public async Task<IActionResult> PostProfil(Profil profil)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            _context.Profil.Add(profil);
            await _context.SaveChangesAsync();

            return Ok(profil);
        }

        // Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfil(int id)
        {
            var profil = await _context.Profil.FindAsync(id);

            if(profil == null)
                return NotFound();

            _context.Profil.Remove(profil);
            await _context.SaveChangesAsync();

            return Ok(profil);
        }
    }
}
