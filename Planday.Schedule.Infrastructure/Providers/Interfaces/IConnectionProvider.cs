namespace Planday.Schedule.Infrastructure.Providers.Interfaces
{
    public interface IConnectionProvider
    {
        Task<T> GetAsync<T>(string path, CancellationToken cancellationToken);
    }    
}

