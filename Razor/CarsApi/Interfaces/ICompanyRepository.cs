using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsApi.Interfaces
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetCompanies();
        Task<IEnumerable<Company>> GetCompaniesDetails();
        Task<Company> GetCompany(int id);
        Task<Company> GetCompanyDetails(int id);
        Task<Company> PostCompany(Company entity);
        Task<Company> PutCompany(Company entity);
        Task<Company> DelCompany(Company entity);
        bool CompanyExists(int id);

    }
}
