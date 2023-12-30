#pragma warning disable CS8618
using Proiect.Entities;

namespace Proiect.Models
{
    public class ArticolUtilizatorDto
    {
        public string? UserName { get; set; }
        public List<Articol> Articole {  get; set; }
    }
}
