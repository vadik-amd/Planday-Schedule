namespace Planday.Schedule.Queries
{
    public interface IGetEmployeeQuery
    {
        Task<Employee> QueryAsync(long id, CancellationToken cancellationToken);
    }    
}

