using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Proiect.Entities;
using Proiect.Views;

namespace Proiect.Services
{
    public interface IAutentificareService
    {
        public Task CreeazaRoluri();
        public Task<Utilizator?> CreeazaUtilizator(RegisterModel model);
        public JwtSecurityToken GetToken(List<Claim> claims);
        public Task<bool> VerificaUtilizator(RegisterModel model);
        public Task AtribuieRoluri(Utilizator utilizator, string rol);
    }
}
