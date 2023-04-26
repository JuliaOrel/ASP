using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using WeatherBot.Services;

namespace WeatherBot.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BotController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post(
           [FromBody] Update update,
           [FromServices] UpdateHandlerService updateHandlerService,
           CancellationToken cancellationToken)
        {
            await updateHandlerService.HandlerUpdateAsync(update, cancellationToken);
            return Ok();
        }
    }
}
