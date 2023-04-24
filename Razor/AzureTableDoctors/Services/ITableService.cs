using AzureTableDoctors.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureTableDoctors.Services
{
    public interface ITableService
    {
        Task<IEnumerable<Medicine>> GetMedicines(string category = null);
        Task<Medicine> GetMedicine(string id, string category);
        Task<Medicine> UpsertMedicine(Medicine medicine);
        Task DeleteMedicine(string id, string category);
    }
}
