using AutoMapper;
using CarsApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsApi.Services
{
    public class CompanyService: ICompanyService
    {
        private readonly IMapper _mapper;
        private readonly ICompanyRepository _companyRepository;
        public CompanyService(IMapper mapper, ICompanyRepository companyRepository)
        {
            _mapper = mapper;
            _companyRepository = companyRepository;
        }
    }
}
