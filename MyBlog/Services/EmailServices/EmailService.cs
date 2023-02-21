using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Services.EmailServices
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string from, string to, string subject, string html)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("Site Administration", from));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            SmtpHiddenInfo smtpHiddenInfo = new SmtpHiddenInfo(); //_configuration.Get<SmtpHiddenInfo>();
            _configuration.GetSection("SmtpHiddenInfo").Bind(smtpHiddenInfo);

            // send email
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(
                smtpHiddenInfo.Host,
                smtpHiddenInfo.Port,
                (SecureSocketOptions)smtpHiddenInfo.SecureSocketOptions);

            // Exeption O_o !!!
            await smtp.AuthenticateAsync(
                smtpHiddenInfo.User,
                smtpHiddenInfo.Password);

            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);


        }
    }
}
