using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proiect.ContextModels;
using Proiect.Entities;
using Proiect.Models;
using Proiect.Repositories;

namespace Proiect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticoleController : ControllerBase
    {
        private readonly ProiectContext _context;
        private readonly IArticolRepository _articolRepository;

        public ArticoleController(ProiectContext context, IArticolRepository articolRepository)
        {
            _context = context;
            _articolRepository = articolRepository;
        }

        // Get
        [HttpGet]
        public async Task<IActionResult> GetArticole()
        {
            if(_context.Articol == null)
                return NotFound();

            var articole = await _articolRepository.GetArticoleAsync();

            if(articole == null)
                return NotFound();

            return Ok(articole);
        }

        // Get grupate dupa autor ordonate dupa UtilizatorId si articolele ordonate dupa titlu
        [HttpGet("grupat_dupa")]
        public async Task<IActionResult> GetArticoleGrupate()
        {
            if(_context == null)
                return NotFound();

            var articole = await _articolRepository.GetArticoleGrupateAsync();

            if (articole == null)
                return NotFound();

            return Ok(articole);
        }

        // Get articole scrise de un anumit autor ordonate alfabtic
        [HttpGet("scris_de/{utilizatorId}")]
        public async Task<IActionResult> GetArticoleAutor(string utilizatorId)
        {
            var articole = await _articolRepository.GetArticolAutorAsync(utilizatorId);

            if(articole == null)
                return NotFound();

            return Ok(articole);
        }

        // Get cu id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetArticol(int id)
        {
            var articol = await _articolRepository.GetArticolAsync(id);

            if (articol == null)
                return NotFound();

            return NoContent();
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

            await _articolRepository.PutArticolAsync(articol);
            return Ok(articol);
        }

        // Post
        [HttpPost]
        public async Task<IActionResult> PostArticol(Articol articol)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            await _articolRepository.PostArticolAsync(articol);
            return NoContent();
        }

        // Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticol(int id)
        {
            var articol = await _context.Articol.FindAsync(id);

            if(articol == null) 
                return NotFound();

            await _articolRepository.DeleteArticolAsync(articol);

            return NoContent();
        }
    }
}
