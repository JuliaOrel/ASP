using AzureTableExample.Models;
using AzureTableExample.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureTableExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ITableService _tableService;
        public ProductsController(ITableService tableService)
        {
            _tableService = tableService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _tableService.GetProducts();
            return Ok(products);
        }

        [HttpGet("{category}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(string category)
        {
            var products = await _tableService.GetProducts(category);
            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult<Product>>PostProduct([FromBody] Product product)
        {
            string id = Guid.NewGuid().ToString();
            product.Id = id;
            product.RowKey = id;
            product.PartitionKey = product.Category;
            var createdProduct = await _tableService.UpsertProduct(product);
            return CreatedAtAction(nameof(GetProducts), createdProduct);
        }

        [HttpPut]
        public async Task<ActionResult<Product>> PutProduct([FromBody] Product product)
        {
            product.PartitionKey = product.Category;
            product.RowKey = product.Id;
            var updatedOrAddedProduct = await _tableService.UpsertProduct(product);
            return Ok(updatedOrAddedProduct);
        }
        [HttpDelete]
        public async Task<ActionResult<Product>> DeleteProduct(string id, string category)
        {
            await _tableService.DeleteProduct(id, category);
            return NoContent();
        }
    }
}
