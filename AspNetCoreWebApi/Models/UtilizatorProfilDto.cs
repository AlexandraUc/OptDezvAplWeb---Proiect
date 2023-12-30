#pragma warning disable CS8618

using Proiect.Entities;

namespace Proiect.Models
{
    public class UtilizatorProfilDto
    {
        public string UserName { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public ICollection<Articol>? Articole { get; set;}
    }
}
