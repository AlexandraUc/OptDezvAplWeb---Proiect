using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Proiect.Entities;
using Proiect.Roles;
using Proiect.Views;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Proiect.Services
{
    public class AutentificareService : IAutentificareService
    {
        private readonly UserManager<Utilizator> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public AutentificareService(UserManager<Utilizator> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }
        public async Task CreeazaRoluri()
        {
            if (!await _roleManager.RoleExistsAsync(RolUtilizator.Admin))
                await _roleManager.CreateAsync(new IdentityRole(RolUtilizator.Admin));
            if (!await _roleManager.RoleExistsAsync(RolUtilizator.Autor))
                await _roleManager.CreateAsync(new IdentityRole(RolUtilizator.Autor));
            if (!await _roleManager.RoleExistsAsync(RolUtilizator.Viewer))
                await _roleManager.CreateAsync(new IdentityRole(RolUtilizator.Viewer));
        }

        public async Task<Utilizator?> CreeazaUtilizator(RegisterModel model)
        {
            Utilizator utilizator = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };

            var rezultat = await _userManager.CreateAsync(utilizator, model.Password);
            if (!rezultat.Succeeded)
                return null;

            return utilizator;
        }
        public JwtSecurityToken GetToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: claims,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
        public async Task<bool> VerificaUtilizator(RegisterModel model)
        {
            var verifica = await _userManager.FindByNameAsync(model.Username);

            if (verifica != null)
                return true;
            return false;
        }
        public async Task AtribuieRoluri(Utilizator utilizator, string rol)
        {
            // Toti utilizatorii au rolul de Viewer
            if (await _roleManager.RoleExistsAsync(RolUtilizator.Viewer))
                await _userManager.AddToRoleAsync(utilizator, RolUtilizator.Viewer);

            if(rol == "Autor")
            {
                if (await _roleManager.RoleExistsAsync(RolUtilizator.Autor))
                    await _userManager.AddToRoleAsync(utilizator, RolUtilizator.Autor);
            }
            else if(rol == "Admin")
            {
                if (await _roleManager.RoleExistsAsync(RolUtilizator.Admin))
                    await _userManager.AddToRoleAsync(utilizator, RolUtilizator.Admin);
                if (await _roleManager.RoleExistsAsync(RolUtilizator.Autor))
                    await _userManager.AddToRoleAsync(utilizator, RolUtilizator.Autor);
            }
        }
    }
}
