using ManageLots.Server.Services;
using ManageLots.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageLots.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LotsController : ControllerBase
    {
        private readonly AzureQueueService _azureQueueService;
        public LotsController(AzureQueueService azureQueueService)
        {
            _azureQueueService = azureQueueService;
        }

        [HttpGet]
        public async Task<IEnumerable<LotGetModel>>GetLots()
        {
            for (int i = 0; i < 32; i++)
            {
                await _azureQueueService.AddLot(new LotAddModel
                {
                    Currency=Currency.UAH+i%3,
                    Amount=123+i,
                    Seller="Dan"+i,
                });
            }

            IEnumerable<LotGetModel> lots = await _azureQueueService.GetAllLots();
            return lots;
        }
    }
}
