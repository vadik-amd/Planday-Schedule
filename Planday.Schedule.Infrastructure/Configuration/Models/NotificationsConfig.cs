namespace Planday.Schedule.Infrastructure.Configuration.Models
{
    public class NotificationsConfig
    {
        public NotificationMessage AssignShift { get; set; }
    }

    public class NotificationMessage
    {
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}