using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meeting2_Services.Services
{
    public class SmsMessageService : IMessageService
    {
        public string SendMessage()
        {
            return "Send SMS";
        }
    }
}
