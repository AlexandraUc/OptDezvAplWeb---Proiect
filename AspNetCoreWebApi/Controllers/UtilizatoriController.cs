using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proiect.ContextModels;
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
        [HttpGet]
        [Authorize(Roles = "Admin")]
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
        [HttpGet("cu_profil/{userName}")]
        public async Task<IActionResult> GetUtilizatorProfil(string userName)
        {
            var utInfo = await _utilizatorRepository.GetUtilizatorProfilDtoAsync(userName);

            if (utInfo == null)
                return NotFound();

            return Ok(utInfo);
        }

        // Get cu id
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUtilizator(string id)
        {
            var utilizator = await _utilizatorRepository.GetUtilizatorAsync(id);
            
            if(utilizator == null)
                return NotFound();

            return Ok(utilizator);
        }

        // Delete de admin, poate sterge orice utilizator 
        // De modificat sa stearga si profilul si articolele daca exista
        [HttpDelete("{userName}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUtilizator(string userName)
        {
            if(await _utilizatorRepository.DeleteUtilizatorAsync(userName))
                return Ok();
            else
                return NotFound();
        }

    }
}
