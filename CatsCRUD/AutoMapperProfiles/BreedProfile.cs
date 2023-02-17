using AutoMapper;
using CatsCRUD.Data.Entities;
using CatsCRUD.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatsCRUD.AutoMapperProfiles
{
    public class BreedProfile:Profile
    {
        public BreedProfile()
        {
            CreateMap<Breed, BreedDTO>().ReverseMap();
        }
    }
}
