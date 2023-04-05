using CarsShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsBlazorClient.HttpServices
{
    public interface ICompanyHttpService
    {
        Task<List<CompanyDTO>> GetCompanies();
        Task<List<CompanyDetailsDTO>> GetCompaiesDetails();
    }
}
