using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsBlazorClient.HttpServices
{
    interface ICompanyHttpService
    {
        Task<List<CompanyDTO>> GetCompanies();
        Task<List<CompanyDetailsDTO>> GetCompaiesDetails();
    }
}
