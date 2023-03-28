using AutoMapper;
using CountriesCities.Data.Entities;
using CountriesCitiesShared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountriesCities.AutoMapperProfiles
{
    public class CityProfile:Profile
    {
        public CityProfile()
        {
            CreateMap<City, CityDTO>().ReverseMap();
            CreateMap<City, CityDetailsDTO>()
                .ForMember(dest => dest.CountryName, opt => opt.MapFrom(sourse => sourse.Country.Name))
                .ForMember(dest => dest.CountryCode, opt => opt.MapFrom(sourse => sourse.Country.Code))
                .ReverseMap();
        }
    }
}
