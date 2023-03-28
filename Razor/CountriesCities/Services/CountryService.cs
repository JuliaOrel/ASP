using AutoMapper;
using CountriesCities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountriesCities.Services
{
    public class CountryService: ICountryService
    {
        private readonly IMapper _mapper;
        private readonly ICityRepository _cityRepository;
        public CountryService(IMapper mapper, ICityRepository cityRepository)
        {
            _mapper = mapper;
            _cityRepository = cityRepository;
        }
    }
}
