using Proiect.Entities;
using Proiect.ContextModels;
using Microsoft.EntityFrameworkCore;
using Proiect.Services;

namespace Proiect.Repositories;

public class ProfilRepository: IProfilRepository
{
    private readonly ProiectContext _context;
    private readonly IUtilizatorService _utilizatorService;
    public ProfilRepository(ProiectContext context, IUtilizatorService utilizatorService)
    {
        _context = context;
        _utilizatorService = utilizatorService;
    }
    public async Task<ICollection<Profil>> GetProfiluriAsync()
    {
        return await _context.Profil.Include(p => p.Articole).ToListAsync();
    }
    public async Task<Profil?> GetProfilAsync(int id)
    {
        return await _context.Profil.Include(p => p.Articole).
                                    FirstOrDefaultAsync(x => x.Id == id);
    }
    public async Task<Profil?> GetProfilUtilizatorAsync(string utilizatorId)
    {
        var profil = await _context.Profil.Where(p => p.UtilizatorId == utilizatorId).ToListAsync();

        if (!profil.Any())
            return null;

        return profil[0];
    }
    public async Task<bool> PutProfilAsync(string? userName, Profil profil)
    {
        var utilizator = await _utilizatorService.GetUtilizator(userName);

        var pr = await _context.Profil.Where(p => p.UtilizatorId == utilizator.Id).ToListAsync();

        // Utilizatorul nu are profil
        if (!pr.Any())
            return false;

        pr[0].Nume = profil.Nume;
        pr[0].Prenume = profil.Prenume;
        pr[0].Bio = profil.Bio;

        _utilizatorService.SeteazaUtilizatorProfil(utilizator, pr[0]);

        _context.Profil.Update(pr[0]);
        await _context.SaveChangesAsync();

        return true;
    }
    public async Task<bool> PostProfilAsync(string? userName, Profil profil)
    {
        var utilizator = await _utilizatorService.GetUtilizator(userName);

        var pr = await _context.Profil.Where(p => p.UtilizatorId == utilizator.Id).ToListAsync();

        // Utilizatorul are deja profil
        if (pr.Any())
            return false;

        _utilizatorService.SeteazaUtilizatorProfil(utilizator, profil);

        _context.Profil.Add(profil);
        await _context.SaveChangesAsync();

        return true;
    }
    public async Task DeleteProfilAsync(Profil profil)
    {
        _context.Profil.Remove(profil);
        await _context.SaveChangesAsync();
    }

    public void DeleteProfilFaraSave(Profil profil)
    {
        _context.Profil.Remove(profil);
    }
    public async Task<bool> DeleteProfilUtilizatorAsync(string userName)
    {
        var utilizator = await _utilizatorService.GetUtilizator(userName);

        var profil = await _context.Profil.Where(p => p.UtilizatorId == utilizator.Id).ToListAsync();

        if(!profil.Any())
            return false;

        _context.Profil.Remove(profil[0]);
        await _context.SaveChangesAsync();

        return true;
    }
}
