using AutoMapper;
using CatsCRUD.Data.Entities;
using CatsCRUD.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatsCRUD.AutoMapperProfiles
{
    public class CatProfile:Profile
    {
        public CatProfile()
        {
            CreateMap<Cat, CatDTO>().ReverseMap();
        }
    }
}
