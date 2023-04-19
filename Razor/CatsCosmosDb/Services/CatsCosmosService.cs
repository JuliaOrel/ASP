using CatsCosmosDb.Models;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatsCosmosDb.Services
{
    public class CatsCosmosService: ICatsCosmosService
    {
        private readonly Container _container;
        public CatsCosmosService(CosmosClient cosmosClient, string databaseName, string containerName)
        {
            _container = cosmosClient.GetContainer(databaseName, containerName);
        }

        public async Task<Cat> AddAsync(Cat newCat)
        {
            var addedCat = await _container.CreateItemAsync<Cat>(newCat, new PartitionKey(newCat.Name));
            return addedCat;
        }

        public async Task<Cat> DeleteAsync(string id, string name)
        {
            var deletedCat = await _container.DeleteItemAsync<Cat>(id, new PartitionKey(name));
            return deletedCat;
        }

        public async Task<IEnumerable<Cat>> GetAsync(string sqlCosmosQuery)
        {
            //var query = _container.GetItemQueryIterator<Cat>(new QueryDefinition(sqlCosmosQuery));
            //List<Cat> cats = new List<Cat>();
            //while(query.HasMoreResults)
            //{
            //    var response = await query.ReadNextAsync();
            //    cats.AddRange(response);

            //}
            //return cats;
            IQueryable<Cat> queryIterator = _container.GetItemLinqQueryable<Cat>(allowSynchronousQueryExecution:true);
            var result = queryIterator.ToList();
            return result;
        }

        public async Task<Cat> UpdateAsync(Cat catToUpdate)
        {
            var editedCat = await _container
                .UpsertItemAsync<Cat>(catToUpdate, new PartitionKey(catToUpdate.Name));
            return editedCat;
        }
    }
}
