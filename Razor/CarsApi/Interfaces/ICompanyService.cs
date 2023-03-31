using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsApi.Interfaces
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyDTO>> GetCompanies();
        Task<IEnumerable<CompanyDetailsDTO>> GetCompaniesDetails();
        Task<CompanyDTO> GetCompany(int id);
        Task<CompanyDetailsDTO> GetCompanyDetails(int id);
        Task<CompanyDTO> PostCompany(CompanyDTO company);
        Task<CompanyDTO> PutCompany(CompanyDTO company);
        Task<CompanyDTO> DelCompany(int id);
        bool CompanyExists(int id);


    }
}
