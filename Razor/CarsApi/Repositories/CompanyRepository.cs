using CarsApi.Data;
using CarsApi.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shared.Models;
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

        public async Task<Company> DelCompany(Company entity)
        {
            var deletedEntity = _context.Companies.Remove(entity);
            await SaveChangesAsync();
            return deletedEntity.Entity;
        }

        public async Task<IEnumerable<Company>> GetCompanies()
        {
            var entities = _context.Companies
                .Where(c => c.IsDeleted == false);
            return await entities.ToListAsync();
        }

        public async Task<IEnumerable<Company>> GetCompaniesDetails()
        {
            var entities = _context.Companies
               .Where(c => c.IsDeleted == false);
            foreach (Company company in entities)
            {
                await _context.Cars.Where(c => c.CompanyId == company.Id && c.IsDeleted == false).LoadAsync();
            }
            return await entities.ToListAsync();
        }

        public async Task<Company> GetCompany(int id)
        {
            Company entity = await _context.Companies
                 .FirstOrDefaultAsync(c => c.Id == id && c.IsDeleted == false);
            return entity;
        }

        public async Task<Company> GetCompanyDetails(int id)
        {
            Company entity = await _context.Companies
                 .FirstOrDefaultAsync(c => c.Id == id && c.IsDeleted == false);
            if(entity is null)
            {
                return null;
            }
            await _context.Cars.Where(c => c.CompanyId == entity.Id && c.IsDeleted == false)
                .LoadAsync();
            return entity;
        }

        public async Task<Company> PostCompany(Company entity)
        {
            var addedEntity = await _context.Companies.AddAsync(entity);
            await SaveChangesAsync();
            return addedEntity.Entity;
        }

        private async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Company> PutCompany(Company entity)
        {
            var updatedEntity = _context.Companies.Update(entity);
            await SaveChangesAsync();
            return updatedEntity.Entity;
        }

        public bool CompanyExists(int id)
        {
            return _context.Companies.Any(c => c.Id == id);
        }
    }
}
