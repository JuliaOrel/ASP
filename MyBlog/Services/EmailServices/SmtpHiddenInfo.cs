using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Services.EmailServices
{
    public class SmtpHiddenInfo
    {
        public string Host { get; set; } 
        public int Port { get; set; }
        public int SecureSocketOptions { get; set; }
        public string User { get; set; } 
        public string Password { get; set; }

    }
}
