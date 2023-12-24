using Microsoft.EntityFrameworkCore;
using Proiect.Entities;

namespace Proiect.ContextModels
{
    public class ProiectContext: DbContext
    {
        public DbSet<Articol> Articol { get; set; }
        public DbSet<Utilizator> Utilizator { get; set; }
        public DbSet<Profil> Profil { get; set; }
        public ProiectContext(DbContextOptions<ProiectContext> options) : base(options) { }
    }
}
