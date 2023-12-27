using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Proiect.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Proiect.ContextModels
{
    public class ProiectContext: IdentityDbContext<IdentityUser>
    {
        public DbSet<Articol> Articol { get; set; }
        public DbSet<Utilizator> Utilizator { get; set; }
        public DbSet<Profil> Profil { get; set; }
        public ProiectContext(DbContextOptions<ProiectContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
