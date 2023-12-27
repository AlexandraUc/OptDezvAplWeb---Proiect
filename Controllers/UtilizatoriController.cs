using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Proiect.ContextModels;
using Proiect.Entities;
using Proiect.Repositories;

namespace Proiect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilizatoriController : ControllerBase
    {
        private readonly ProiectContext _context;
        private readonly IUtilizatorRepository _utilizatorRepository;

        public UtilizatoriController(ProiectContext context, IUtilizatorRepository utilizatorRepository)
        {
            _context = context;
            _utilizatorRepository = utilizatorRepository;
        }

        // Get
        // [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetUtilizatori()
        {
            if (_context.Utilizator == null)
                return NotFound();

            var utilizatori = await _utilizatorRepository.GetUtilizatoriAsync();

            if(utilizatori == null)
                return NotFound();

            return Ok(utilizatori);
        }

        // Get cu date din profil
        // [Authorize(Roles = "Admin")]
        [HttpGet("cu_profil/{id}")]
        public async Task<IActionResult> GetUtilizatorProfil(string id)
        {
            var utInfo = await _utilizatorRepository.GetUtilizatorProfilDtoAsync(id);
            return Ok(utInfo);
        }

        // Get cu id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUtilizator(string id)
        {
            var utilizator = await _utilizatorRepository.GetUtilizatorAsync(id);
            
            if(utilizator == null)
                return NotFound();

            return Ok(utilizator);
        }

        // Put
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUtilizator(string id, Utilizator utilizator)
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

            await _utilizatorRepository.PutUtilizatorAsync(utilizator);
            return Ok(utilizator);
        }

        // Post
        [HttpPost]
        public async Task<IActionResult> PostUtilizator(Utilizator utilizator)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            await _utilizatorRepository.PostUtilizatorAsync(utilizator);
            return NoContent();
        }

        // Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUtilizator(string id)
        {
            var utilizator = await _context.Utilizator.FindAsync(id);

            if(utilizator == null)
                return NotFound();

            await _utilizatorRepository.DeleteUtilizatorAsync(utilizator);
            return NoContent();
        }
    }
}
