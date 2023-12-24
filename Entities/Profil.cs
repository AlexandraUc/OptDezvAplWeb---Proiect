namespace Proiect.Entities
{
    public class Profil
    {
        public int Id { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Bio { get; set;}

        // One to one cu Utilizator
        public int? UtilizatorId { get; set; }
        public Utilizator? Utilizator { get; set; }

        // Many to many cu Articol
        public ICollection<Articol>? Articole {  get; set; } = new List<Articol>();
        public Profil(int Id, string Nume, string Prenume, string Bio, int? UtilizatorId) {
            this.Id = Id;
            this.Nume = Nume;
            this.Prenume = Prenume;
            this.Bio = Bio;
            this.UtilizatorId = UtilizatorId;
        }
    }
}
