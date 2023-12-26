#pragma warning disable CS8618
using Proiect.Entities;

namespace Proiect.Models
{
    public class ArticolUtilizatorDto
    {
        public int? UtilizatorId { get; set; }
        public List<Articol> Articole {  get; set; }
    }
}
