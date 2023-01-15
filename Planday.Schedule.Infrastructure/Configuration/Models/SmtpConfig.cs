namespace Planday.Schedule.Infrastructure.Configuration.Models;

public class SmtpConfig
{
    public string Host { get; set; }
    public int Port { get; set; }
    public Credential UserCredential { get; set; }
    public string FromAddress { get; set; }
}

public class Credential
{
    public string UserName { get; set; }
    public string Password { get; set; }
}