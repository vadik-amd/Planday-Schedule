namespace Planday.Schedule.Services
{
    public interface INotificationService
    {
        Task SendNotificationAsync(string emailAddress, CancellationToken cancellationToken);
    }    
}

