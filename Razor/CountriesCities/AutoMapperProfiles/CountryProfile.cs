using AutoMapper;
using CountriesCities.Data.Entities;
using CountriesCitiesShared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountriesCities.AutoMapperProfiles
{
    public class CountryProfile: Profile
    {
        public CountryProfile()
        {
            CreateMap<Country, CountryDTO>().ReverseMap();
            CreateMap<Country, CountryDetailsDTO>().ReverseMap();
        }
    }
}
