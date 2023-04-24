using AzureTableExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureTableExample.Services
{
    public interface ITableService
    {
        Task<IEnumerable<Product>> GetProducts(string category = null);
        Task<Product> GetProduct(string id, string category);
        Task<Product> UpsertProduct(Product product);
        Task DeleteProduct(string id, string category);

    }
}
