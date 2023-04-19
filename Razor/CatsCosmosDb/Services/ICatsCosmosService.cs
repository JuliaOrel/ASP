using CatsCosmosDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatsCosmosDb.Services
{
    public interface ICatsCosmosService
    {
        Task<IEnumerable<Cat>> GetAsync(string sqlCosmosQuery);
        Task<Cat> AddAsync(Cat newCat);
        Task<Cat> UpdateAsync(Cat catToUpdate);
        Task<Cat> DeleteAsync(string id, string name);
    }
}
