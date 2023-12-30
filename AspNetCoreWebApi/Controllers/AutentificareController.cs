using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Proiect.Entities;
using Proiect.Views;
using Proiect.Roles;
using Proiect.Services;
using Microsoft.AspNetCore.Authorization;

namespace Proiect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutentificareController : ControllerBase
    {
        private readonly UserManager<Utilizator> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAutentificareService _autentificareService;
        public AutentificareController(UserManager<Utilizator> userManager, RoleManager<IdentityRole> roleManager, IAutentificareService autentificareService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _autentificareService = autentificareService;
        }

        /*
        [HttpGet]
        public async Task<IActionResult> GetName()
        {
            var name = User.FindFirstValue(ClaimTypes.Name);
            return Ok(name);
        }
        */

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            // Daca exista utilizatorul si e validata parola
            var utilizator = await _userManager.FindByNameAsync(model.Username);
            if(utilizator != null && await _userManager.CheckPasswordAsync(utilizator, model.Password))
            {
                var roluri = await _userManager.GetRolesAsync(utilizator);

                // Creaza claims pt utilizatorul continand numele, un identificator unic si rolurile sale
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, utilizator.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                foreach(var rol in roluri) {
                    claims.Add(new Claim(ClaimTypes.Role, rol));
                }

                // Creeaza un token bazat pe claims ale utilizatorului
                // Folosit pentru a avea acces la anumite metode
                var token = _autentificareService.GetToken(claims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }

            return Unauthorized();
        }

        [HttpPost]
        [Route("register-autor")]
        public async Task<IActionResult> RegisterAutor([FromBody] RegisterModel model)
        {
            // Verifica daca exista deja utilizator cu acest nume
            if(await _autentificareService.VerificaUtilizator(model))
                return StatusCode(StatusCodes.Status500InternalServerError, new Rezultat { Statut = "Eroare", Mesaj = "Deja exista utilizatorul" });

            // Creeaza utilizator
            var utilizator = await _autentificareService.CreeazaUtilizator(model);

            // Verifica daca a fost creat
            if(utilizator == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Rezultat { Statut = "Eroare", Mesaj = "Inregistrare esuata" });

            // Creaza rolurile daca nu exista
            await _autentificareService.CreeazaRoluri();

            // Atribuie rolurile utilizatorului
            await _autentificareService.AtribuieRoluri(utilizator, RolUtilizator.Autor);

            return Ok(new Rezultat { Statut = "Succes", Mesaj = "Utilizator creat!" });
        }

        [HttpPost]
        [Route("register-viewer")]
        public async Task<IActionResult> RegisterViewer([FromBody] RegisterModel model)
        {
            if (await _autentificareService.VerificaUtilizator(model))
                return StatusCode(StatusCodes.Status500InternalServerError, new Rezultat { Statut = "Eroare", Mesaj = "Deja exista utilizatorul" });

            var utilizator = await _autentificareService.CreeazaUtilizator(model);

            if(utilizator == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Rezultat { Statut = "Eroare", Mesaj = "Inregistrare esuata" });

            await _autentificareService.CreeazaRoluri();

            await _autentificareService.AtribuieRoluri(utilizator, RolUtilizator.Viewer);

            return Ok(new Rezultat { Statut = "Succes", Mesaj = "Utilizator creat!" });
        }

        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            if (await _autentificareService.VerificaUtilizator(model))
                return StatusCode(StatusCodes.Status500InternalServerError, new Rezultat { Statut = "Eroare", Mesaj = "Deja exista utilizatorul" });

            var utilizator = await _autentificareService.CreeazaUtilizator(model);

            if(utilizator == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Rezultat { Statut = "Eroare", Mesaj = "Inregistrare esuata" });

            await _autentificareService.CreeazaRoluri();

            await _autentificareService.AtribuieRoluri(utilizator, RolUtilizator.Admin);
            
            return Ok(new Rezultat { Statut = "Succes", Mesaj = "Utilizator creat!" });
        }

        [HttpPut]
        [Authorize]
        [Route("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
        {
            // Daca exista utilizatorul si e validata parola
            var utilizator = await _userManager.FindByNameAsync(model.UserName);

            if (utilizator == null)
                return StatusCode(StatusCodes.Status404NotFound, new Rezultat { Statut = "Eroare", Mesaj = "Nu exista utilizatorul" });

            if(await _userManager.CheckPasswordAsync(utilizator, model.CurrentPassword))
            {
                await _userManager.ChangePasswordAsync(utilizator, model.CurrentPassword, model.NewPassword);
                return Ok(new Rezultat { Statut = "Succes", Mesaj = "Parola schimbata!" });
            }
            else
                return Unauthorized();
        }
    }
}
