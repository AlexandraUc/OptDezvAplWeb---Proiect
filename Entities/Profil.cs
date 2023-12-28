using System.Text.Json.Serialization;

namespace Proiect.Entities
{
    public class Profil
    {
        public int Id { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Bio { get; set;}

        // One to one cu Utilizator
        public string? UtilizatorId { get; set; }

        [JsonIgnore]
        public Utilizator? Utilizator { get; set; }

        // Many to many cu Articol
        public ICollection<Articol>? Articole {  get; set; } = new List<Articol>();

        public Profil() { }
        public Profil(int Id, string Nume, string Prenume, string Bio, string? UtilizatorId) {
            this.Id = Id;
            this.Nume = Nume;
            this.Prenume = Prenume;
            this.Bio = Bio;
            this.UtilizatorId = UtilizatorId;
        }
    }
}
