namespace Planday.Schedule.Queries
{
    public interface IUpdateShiftQuery
    {
        Task<bool> QueryAsync(long shiftId, long employeeId);
    }    
}

