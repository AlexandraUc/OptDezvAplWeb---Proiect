using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proiect.ContextModels;
using Proiect.Entities;

namespace Proiect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilizatoriController : ControllerBase
    {
        private readonly ProiectContext _context;

        public UtilizatoriController(ProiectContext context)
        {
            _context = context;
        }

        // Get
        [HttpGet]
        public async Task<IActionResult> GetUtilizatori()
        {
            if (_context.Utilizator == null)
                return NotFound();

            return Ok(await _context.Utilizator.ToListAsync());
        }

        // Get cu id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUtilizator(int id)
        {
            var utilizator = await _context.Utilizator.FindAsync(id);
            
            if(utilizator == null)
                return NotFound();

            return Ok(utilizator);
        }

        // Put
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUtilizator(int id, Utilizator utilizator)
        {
            // Validare parametri
            if (id != utilizator.Id)
                return BadRequest();

            // Validare tip parametru
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var ut = await _context.Utilizator.FindAsync(id);

            if(ut == null)
                return NotFound();

            // Updateaza utilizatorul dupa ce stim ca exista
            _context.Utilizator.Update(utilizator);
            await _context.SaveChangesAsync();

            // Returneaza utilizatorul modificat
            return Ok(utilizator);
        }

        // Post
        [HttpPost]
        public async Task<IActionResult> PostUtilizator(Utilizator utilizator)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            // Adauga utilizator
            _context.Utilizator.Add(utilizator);
            await _context.SaveChangesAsync();

            // Returneaza noul utilizator
            return Ok(utilizator);
        }

        // Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUtilizator(int id)
        {
            var utilizator = await _context.Utilizator.FindAsync(id);

            if(utilizator == null)
                return NotFound();

            _context.Utilizator.Remove(utilizator);
            await _context.SaveChangesAsync();

            return Ok(utilizator);
        }
    }
}
