using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using Planday.Schedule.Infrastructure.Configuration.Models;
using Planday.Schedule.Infrastructure.Providers.Interfaces;


namespace Planday.Schedule.Infrastructure.Providers
{
    public class EmailProvider : IEmailProvider
    {
        private readonly SmtpClient _smtpClient;
        private readonly SmtpConfig _config;


        public EmailProvider(SmtpClient smtpClient, IOptions<SmtpConfig> options)
        {
            _smtpClient = smtpClient;
            _config = options.Value;
            _smtpClient.Host = _config.Host;
            _smtpClient.Port = _config.Port;
            _smtpClient.Credentials =
                new NetworkCredential(_config.UserCredential.UserName, _config.UserCredential.Password);
        }

        public async Task SendAsync(string subject, string body, string to,
            CancellationToken cancellationToken)
        {
            try
            {
                var email = new MailMessage(_config.FromAddress, to, subject, body);
                await _smtpClient.SendMailAsync(email, cancellationToken);
            }
            catch (Exception)
            {
                //Log exception
            }
        }
    }    
}

