using ProductStore2.Data;
using ProductStore2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductStore2.Services
{
    public class ProductService : IProductservice
    {
        private readonly ProductContext _productContext;
        public ProductService(ProductContext context)
        {
            _productContext = context;
        }
        public IQueryable<Product> GetProducts()
        {
            IQueryable<Product> products = _productContext.Products;
            return products;
        }
        public async Task Buy(Order order)
        {
            _productContext.Orders.Add(order);
            await _productContext.SaveChangesAsync();
        }

       
    }
}
