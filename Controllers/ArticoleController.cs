using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proiect.ContextModels;
using Proiect.Entities;

namespace Proiect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticoleController : ControllerBase
    {
        private readonly ProiectContext _context;

        public ArticoleController(ProiectContext context)
        {
            _context = context;
        }

        // Get
        [HttpGet]
        public async Task<IActionResult> GetArticole()
        {
            if(_context.Articol == null)
                return NotFound();

            var articole = await _context.Articol.ToListAsync();

            return Ok(articole);
        }

        // Get grupate dupa autor ordonate dupa UtilizatorId si articolele ordonate dupa titlu
        [HttpGet("grupat_dupa")]
        public async Task<IActionResult> GetArticoleGrupate()
        {
            if(_context == null)
                return NotFound();

            var groupedArticole = await _context.Articol
                .GroupBy(a => a.UtilizatorId)
                .OrderBy(a => a.Key)
                .Select(grup => new         // Fiecare grup o sa fie reprezentat de un UtilizatorId si o                       
                {                                    // lista de articole ordonate dupa titlu
                    UtilizatorId = grup.Key,
                    Articole = grup.OrderBy(a => a.Titlu).ToList()
                })
                .ToListAsync();

            return Ok(groupedArticole);
        }

        // Get articole scrise de un anumit autor ordonate alfabtic
        [HttpGet("scris_de/{utilizatorId}")]
        public async Task<IActionResult> GetArticoleAutor(int utilizatorId)
        {
            var articole = await _context.Articol.Where(x => x.UtilizatorId == utilizatorId).
                OrderBy(x => x.Titlu).ToListAsync();
            return Ok(articole);
        }

        // Get cu id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetArticol(int id)
        {
            var articol = await _context.Articol.FindAsync(id);

            if (articol == null)
                return NotFound();

            return Ok(articol);
        }

        // Put
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticol(int id, Articol articol)
        {
            if(id != articol.Id)
                return BadRequest();

            if(!ModelState.IsValid)
                return BadRequest();

            var ar = await _context.Articol.FindAsync(id);

            if(ar == null)
                return NotFound();

            _context.Articol.Update(articol);
            await _context.SaveChangesAsync();

            return Ok(articol);
        }

        // Post
        [HttpPost]
        public async Task<IActionResult> PostArticol(Articol articol)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            _context.Articol.Add(articol);
            await _context.SaveChangesAsync();

            return Ok(articol);
        }

        // Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticol(int id)
        {
            var articol = await _context.Articol.FindAsync(id);

            if(articol == null) 
                return NotFound();

            _context.Articol.Remove(articol);
            await _context.SaveChangesAsync();

            return Ok(articol);
        }
    }
}
