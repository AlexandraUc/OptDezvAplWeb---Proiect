using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proiect.ContextModels;
using Proiect.Repositories;
using Proiect.UnitsOfWork;

namespace Proiect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilizatoriController : ControllerBase
    {
        private readonly ProiectContext _context;
        private readonly IUtilizatorRepository _utilizatorRepository;
        private readonly IUtilizatorUnitOfWork _utilizatorUnitOfWork;

        public UtilizatoriController(ProiectContext context, IUtilizatorRepository utilizatorRepository, IUtilizatorUnitOfWork utilizatorUnitOfWork)
        {
            _context = context;
            _utilizatorRepository = utilizatorRepository;
            _utilizatorUnitOfWork = utilizatorUnitOfWork;
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
        [HttpDelete("{userName}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUtilizator(string userName)
        {
            try
            {
                using (var unitOfWork = _utilizatorUnitOfWork)
                {
                    unitOfWork.BeginTransaction();

                    await unitOfWork.StergeArticole(userName);
                    var utilizator = await unitOfWork.StergeProfil(userName);

                    if (utilizator != null)
                        unitOfWork.UtilizatorRepository.DeleteUtilizator(utilizator);

                    unitOfWork.Commit();
                }

                return Ok();
            }
            catch (DbUpdateException dbUpdateEx)
            {
                Console.WriteLine(dbUpdateEx.ToString());
                return BadRequest();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest();
            }
        }

    }
}
