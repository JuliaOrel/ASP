using CarsApi.Data;
using CarsApi.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsApi.Repositories
{
    public class CarRepository: ICarRepository
    {
        private readonly CarsApiContext _context;
        public CarRepository(CarsApiContext context)
        {
            _context = context;
        }

        public bool CityExists(int id)
        {
            return _context.Cars.Any(c => c.Id == id);
        }

        public async Task<Car> DeleteCar(Car entity)
        {
            entity.IsDeleted = true;
            await SaveChangesAsync();
            return entity;
        }

        public async Task<Car> GetCar(int id)
        {
            Car entity = await _context.Cars
                .FirstOrDefaultAsync(c => c.Id == id && c.IsDeleted == false);
            if (entity is null)
            {
                return null;
            }
            
           return entity;

        }

        public async Task<Car> GetCarDetails(int id)
        {
            Car entity = await _context.Cars
                .FirstOrDefaultAsync(c => c.Id == id && c.IsDeleted == false);
            if (entity is null)
            {
                return null;
            }
            await _context.Companies.Where(c => c.Id == entity.CompanyId && c.IsDeleted == false)
                .LoadAsync();
            return entity;
        }

        public async Task<IEnumerable<Car>> GetCars()
        {
            IQueryable<Car> entities = _context.Cars
                .Where(c => c.IsDeleted == false);
            return await entities.ToListAsync();
        }

        public async Task<IEnumerable<Car>> GetCarsDetails()
        {
            IQueryable<Car> entities = _context.Cars.Where(c => c.IsDeleted == false);
            foreach (Car entity in entities)
            {
                await _context.Companies.Where(c => c.Id == entity.CompanyId && c.IsDeleted == false)
                    .LoadAsync();
            }
            return await entities.ToListAsync();
        }

        public async Task<Car> PostCar(Car entity)
        {
            EntityEntry<Car> addedEntity = await _context.Cars.AddAsync(entity);
            await SaveChangesAsync();
            return addedEntity.Entity;
        }

        public async Task<Car> PutCar(Car entity)
        {
            EntityEntry<Car> updatedEntity = _context.Cars.Update(entity);
            await SaveChangesAsync();
            return updatedEntity.Entity;
        }

        private async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
