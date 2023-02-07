using ProductStore2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductStore2.Services
{
    public interface IProductservice
    {
        IQueryable<Product> GetProducts();
        Task Buy(Order order);
    }
}
