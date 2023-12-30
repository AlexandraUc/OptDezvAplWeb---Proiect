using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace Proiect.Entities
{
    public class Articol
    {
        public int Id { get; set; }
        public string Titlu { get; set; }
        public string Continut { get; set; }

        // One to many cu Utilizator, autorul articolului
        public string UtilizatorId { get; set; }

        [JsonIgnore]
        public Utilizator? Utilizator { get; set; }

        // Many to many cu Profil (mai multe profile pot avea la favorite mai multe articole)
        // Ignora lista la serializare
        [JsonIgnore]
        public ICollection<Profil>? Profiluri { get; set; }
        public Articol() { }
        public Articol(int Id, string Titlu, string Continut, string UtilizatorId) {
            this.Id = Id;
            this.Titlu = Titlu;
            this.Continut = Continut;
            this.UtilizatorId = UtilizatorId;
        }
    }
}
