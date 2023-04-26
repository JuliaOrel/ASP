using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace WebHookTelegramBot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BotController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _token;
        public BotController(IConfiguration configuration)
        {
            _configuration = configuration;
            _token = _configuration.GetValue<string>("Token");

        }

        [HttpPost]
        public async Task<IActionResult> PostResult([FromBody] Update update)
        {
            TelegramBotClient botClient = new(_token);
            if(update.Message is not Message message)
            {
                return BadRequest();
            }
            await botClient.SendTextMessageAsync(message.From.Id, "Hello");
            return Ok();
        }
    }
}
