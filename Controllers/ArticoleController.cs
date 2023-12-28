using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Proiect.ContextModels;
using Proiect.Entities;
using Proiect.Models;
using Proiect.Repositories;
using Proiect.UnitsOfWork;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Proiect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticoleController : ControllerBase
    {
        private readonly ProiectContext _context;
        private readonly IArticolRepository _articolRepository;
        private readonly IArticolProfilUnitOfWork _articolProfilUnitOfWork;
        private readonly IMapper _mapper;

        public ArticoleController(ProiectContext context, IArticolProfilUnitOfWork articolProfilUnitOfWork, IArticolRepository articolRepository, IMapper mapper)
        {
            _context = context;
            _articolRepository = articolRepository;
            _articolProfilUnitOfWork = articolProfilUnitOfWork;
            _mapper = mapper;
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
        [HttpGet("scris_de/{userName}")]
        public async Task<IActionResult> GetArticoleAutor(string userName)
        {
            var articole = await _articolRepository.GetArticolAutorAsync(userName);

            if(articole == null)
                return NotFound();

            return Ok(articole);
        }

        // Get cu titlu
        [HttpGet("{titlu}")]
        public async Task<IActionResult> GetArticol(string titlu)
        {
            var articol = await _articolRepository.GetArticolAsync(titlu);

            if (articol == null)
                return NotFound();

            return Ok(articol);
        }

        // Put
        [HttpPut("{id}")]
        [Authorize(Roles = "Autor")]
        public async Task<IActionResult> PutArticol(int id, ArticolFaraIdDto articol)
        {
            var userName = User.Identity.Name;

            var ar = await _articolRepository.PutArticolAsync(userName, id, _mapper.Map<Articol>(articol));

            if(ar == null)
                return NotFound();

            return Ok(articol);
        }

        // Post
        [HttpPost]
        [Authorize(Roles = "Autor")]
        public async Task<IActionResult> PostArticol(ArticolFaraIdDto articol)
        {
            var userName = User.Identity.Name;

            var ar = await _articolRepository.PostArticolAsync(userName, _mapper.Map<Articol>(articol));

            if(ar == null)
                return NotFound();

            return Ok();
        }

        // Delete de admin (poate sterge orice articol)
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteArticol(int id)
        {
            var ok = await _articolRepository.DeleteArticolAsync(id);

            if (ok == false)
                return NotFound();

            return Ok();
        }

        // Delete de autor (isi poate sterge doar propriul articol)
        [HttpDelete("dupa/{titlu}")]
        [Authorize(Roles = "Autor")]
        public async Task<IActionResult> DeleteArticolUtilizator(string titlu)
        {
            var articol = await _articolRepository.GetArticolAsync(titlu);

            if (articol == null)
                return NotFound();

            var userName = User.FindFirstValue(ClaimTypes.Name);

            if (await _articolRepository.DeleteArticolUtilizatorAsync(userName, articol))
                return Ok();

            return Unauthorized();
        }
    }
}
