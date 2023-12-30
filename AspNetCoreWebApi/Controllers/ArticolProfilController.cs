#pragma warning disable CS8602

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proiect.UnitsOfWork;

namespace Proiect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticolProfilController: ControllerBase
    {
        private readonly IArticolProfilUnitOfWork _unitOfWork;
        public ArticolProfilController(IArticolProfilUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPut("titlu")]
        [Authorize]
        public async Task<IActionResult> PutArticolProfil(string titlu)
        {
            var userName = User.Identity.Name;

            try
            {
                _unitOfWork.BeginTransaction();

                var utilizator = await _unitOfWork.UtilizatorService.GetUtilizator(userName);

                var profil = await _unitOfWork.ProfilRepository.GetProfilUtilizatorAsync(utilizator.Id);

                var articol = await _unitOfWork.ArticolRepository.GetArticolAsync(titlu);

                if (_unitOfWork.AtribuieArticolProfil(articol, profil))
                {
                    _unitOfWork.Commit();
                    return Ok();
                }
                else
                {
                    _unitOfWork.Rollback();
                    return BadRequest();
                }
            }
            catch(Exception)
            {
                _unitOfWork.Rollback();
                return BadRequest();
            }
        }
    }
}
