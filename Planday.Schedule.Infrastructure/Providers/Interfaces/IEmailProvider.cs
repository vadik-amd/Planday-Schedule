namespace Planday.Schedule.Infrastructure.Providers.Interfaces
{
    public interface IEmailProvider
    {
        Task SendAsync(string subject, string body, string to, CancellationToken cancellationToken);
    }    
}

