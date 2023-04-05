using AutoMapper;
using CarsShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsApi.AutoMapperProfiles
{
    public class CarProfile: Profile
    {
        public CarProfile()
        {
            CreateMap<Car, CarDTO>().ReverseMap();
            CreateMap<Car, CarDetailsDTO>()
            .ForMember(destination => destination.CompanyName,
            opt => opt.MapFrom(
            source => source.Company.Name))
            .ReverseMap();
        }
    }
}
