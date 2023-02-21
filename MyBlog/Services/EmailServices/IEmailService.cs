using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Services.EmailServices
{
    public interface IEmailService
    {
        Task SendEmailAsync(string from, string to, string subject, string html);
    }
}
