using CatsCosmosDb.Models;
using CatsCosmosDb.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatsCosmosDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatsController : ControllerBase
    {
        private readonly ICatsCosmosService _catsCosmosService;
        public CatsController(ICatsCosmosService catsCosmosService)
        {
            _catsCosmosService = catsCosmosService;
        }

        [HttpGet]
        public async Task<IEnumerable<Cat>> Get()
        {
            string sqlCosmosQuery = "SELECT * FROM c";
            var result = await _catsCosmosService.GetAsync(sqlCosmosQuery);
            return result;
        }

        [HttpPost]
        public async Task<Cat>Post(Cat newCat)
        {
            newCat.Id = Guid.NewGuid().ToString();
            Cat result = await _catsCosmosService.AddAsync(newCat);
            return result;
        }

        [HttpPut]
        public async Task<ActionResult<Cat>> Put(Cat catToUpdate)
        {
            var result = await _catsCosmosService.UpdateAsync(catToUpdate);
            return result;
        }

        [HttpDelete]
        public async Task<ActionResult<Cat>> Delete(string id, string name)
        {
            var result = await _catsCosmosService.DeleteAsync(id, name);
            return result;
        }
    }
}
