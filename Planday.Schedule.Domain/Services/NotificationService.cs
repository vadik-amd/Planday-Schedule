using Microsoft.Extensions.Options;
using Planday.Schedule.Infrastructure.Configuration.Models;
using Planday.Schedule.Infrastructure.Providers.Interfaces;
using Planday.Schedule.Services;

namespace Planday.Schedule.Domain.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IEmailProvider _emailProvider;
        private readonly NotificationsConfig _config;
        
        public NotificationService(IEmailProvider emailProvider, IOptions<NotificationsConfig> options)
        {
            _emailProvider = emailProvider;
            _config = options.Value;
        }

        public async Task SendNotificationAsync(string emailAddress, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(emailAddress))
            {
                // Log Warn
                return;
            }
            
            await Task.Run(() => _emailProvider.SendAsync(_config.AssignShift.Subject, _config.AssignShift.Body,
                emailAddress, cancellationToken), cancellationToken);
        }
    }    
}

