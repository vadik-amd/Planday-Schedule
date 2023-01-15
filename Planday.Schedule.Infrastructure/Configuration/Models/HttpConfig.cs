namespace Planday.Schedule.Infrastructure.Configuration.Models;

public class HttpConfig
{
    public string RootUrl { get; set; }
    public int Port { get; set; }
    public string AuthenticationToken { get; set; }
}