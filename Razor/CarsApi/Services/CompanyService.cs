using AutoMapper;
using CarsApi.Interfaces;
using CarsShared.Models;
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

        public bool CompanyExists(int id)
        {
            return _companyRepository.CompanyExists(id);
        }

        public async Task<CompanyDTO> DelCompany(int id)
        {
            var entity = await _companyRepository.GetCompanyDetails(id);//!!!!!
            if(entity is null)
            {
                return null;
            }
            var deletedEntry = await _companyRepository.DelCompany(entity);
            var result = _mapper.Map<CompanyDTO>(deletedEntry);
            return result;
        }

        public async Task<IEnumerable<CompanyDTO>> GetCompanies()
        {
            var entities = await _companyRepository.GetCompanies();
            var result = _mapper.Map<IEnumerable<CompanyDTO>>(entities);
            return result;

        }

        public async Task<IEnumerable<CompanyDetailsDTO>> GetCompaniesDetails()
        {
            var entities = await _companyRepository.GetCompaniesDetails();
            var result = _mapper.Map<IEnumerable<CompanyDetailsDTO>>(entities);
            return result;
        }

        public async Task<CompanyDTO> GetCompany(int id)
        {
            var entity = await _companyRepository.GetCompany(id);
            if(entity is null)
            {
                return null;
            }
            var result = _mapper.Map<CompanyDTO>(entity);
            return result;
        }

        public async Task<CompanyDetailsDTO> GetCompanyDetails(int id)
        {
            var entity = await _companyRepository.GetCompanyDetails(id);
            if (entity is null)
            {
                return null;
            }
            var result = _mapper.Map<CompanyDetailsDTO>(entity);
            return result;
        }

        public async Task<CompanyDTO> PostCompany(CompanyDTO company)
        {
            var entity = _mapper.Map<Company>(company);
            var addedEntity = await _companyRepository.PostCompany(entity);
            var result = _mapper.Map<CompanyDTO>(addedEntity);
            return result;
        }

        public async Task<CompanyDTO> PutCompany(CompanyDTO company)
        {
            var entity = _mapper.Map<Company>(company);
            if(entity is null)
            {
                return null;
            }
            var updatedEntity = await _companyRepository.PutCompany(entity);
            var result = _mapper.Map<CompanyDTO>(updatedEntity);
            return result;
        }
    }
}
