using AutoMapper;
using CarsShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsApi.AutoMapperProfiles
{
    public class CompanyProfile: Profile
    {
        public CompanyProfile()
        {
            CreateMap<Company, CompanyDTO>().ReverseMap();
            CreateMap<Company, CompanyDetailsDTO>().ReverseMap();
        }
    }
}
