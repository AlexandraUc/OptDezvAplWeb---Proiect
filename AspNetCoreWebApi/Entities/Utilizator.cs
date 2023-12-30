using Microsoft.AspNetCore.Identity;

namespace Proiect.Entities
{
    public class Utilizator: IdentityUser
    {
        // One to one cu Profil
        public Profil? Profil { get; set; } 
        
        // One to many cu Articol (un utilizator poate scrie mai multe articole)
        public ICollection<Articol>? Articole { get; set; } = new List<Articol>();
        public Utilizator() {}
    }
}
