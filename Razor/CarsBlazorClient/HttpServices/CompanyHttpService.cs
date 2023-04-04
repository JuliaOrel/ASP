using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsBlazorClient.HttpServices
{
    public class CompanyHttpService : ICompanyHttpService
    {
        public Task<List<CompanyDetailsDTO>> GetCompaiesDetails()
        {
            throw new NotImplementedException();
        }

        public Task<List<CompanyDTO>> GetCompanies()
        {
            throw new NotImplementedException();
        }
    }
}
