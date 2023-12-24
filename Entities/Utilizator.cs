namespace Proiect.Entities
{
    public class Utilizator
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Parola { get; set; }
        public string Email { get; set; }
        public int Rol { get; set; }

        // One to one cu Profil
        public Profil? Profil { get; set; } 
        
        // One to many cu Articol (un utilizator poate scrie mai multe articole)
        public ICollection<Articol>? Articole { get; set; } = new List<Articol>();
        public Utilizator(int Id, string Username, string Parola, string Email, int Rol) {
            this.Id = Id;
            this.Username = Username;
            this.Parola = Parola;
            this.Email = Email;
            this.Rol = Rol;
        }
    }
}
