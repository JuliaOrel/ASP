using CarsApi.Data;
using CarsApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsApi.Repositories
{
    public class CompanyRepository: ICompanyRepository
    {
        private readonly CarsApiContext _context;
        public CompanyRepository(CarsApiContext context)
        {
            _context = context;
        }
    }
}
