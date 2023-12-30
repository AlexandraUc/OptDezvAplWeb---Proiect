using AutoMapper;
using Proiect.Entities;
using Proiect.Models;

namespace Proiect.Profiles
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<ArticolFaraIdDto, Articol>();
            CreateMap<PostProfilDto, Profil>();
        }
    }
}
