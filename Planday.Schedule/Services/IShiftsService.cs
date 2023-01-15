namespace Planday.Schedule.Services
{
    public interface IShiftsService
    {
        Task<IReadOnlyCollection<Shift>> GetShiftsAsync();
        Task<Shift> GetShiftAsync(long id, CancellationToken cancellationToken);
        Task<bool> AddShiftAsync(Shift shift);
        Task<bool> AssignShiftAsync(long shiftId, long employeeId, CancellationToken cancellationToken);
    }    
}

