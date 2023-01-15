namespace Planday.Schedule.Queries
{
    public interface IInsertShiftQuery
    {
        Task<bool> QueryAsync(Shift shift);
    }    
}

