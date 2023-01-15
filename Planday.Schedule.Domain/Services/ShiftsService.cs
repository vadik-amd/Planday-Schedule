using Planday.Schedule.Queries;
using Planday.Schedule.Services;

namespace Planday.Schedule.Domain.Services
{
    public class ShiftsService : IShiftsService
    {
        private readonly IGetAllShiftsQuery _allShiftsQuery;
        private readonly IGetShiftQuery _getShiftQuery;
        private readonly IInsertShiftQuery _insertShiftQuery;
        private readonly IUpdateShiftQuery _updateShiftQuery;
        private readonly IGetEmployeeQuery _getEmployeeQuery;
        private readonly INotificationService _notificationService;
        
        public ShiftsService(
            IGetAllShiftsQuery allShiftsQuery,
            IGetShiftQuery getShiftQuery,
            IInsertShiftQuery insertShiftQuery,
            IUpdateShiftQuery updateShiftQuery,
            IGetEmployeeQuery getEmployeeQuery,
            INotificationService notificationService)
        {
            _allShiftsQuery = allShiftsQuery;
            _getShiftQuery = getShiftQuery;
            _insertShiftQuery = insertShiftQuery;
            _updateShiftQuery = updateShiftQuery;
            _getEmployeeQuery = getEmployeeQuery;
            _notificationService = notificationService;
        }
        public Task<IReadOnlyCollection<Shift>> GetShiftsAsync()
        {
            return _allShiftsQuery.QueryAsync();
        }

        public async Task<Shift> GetShiftAsync(long id, CancellationToken cancellationToken)
        {
            var shift = await _getShiftQuery.QueryAsync(id);

            if (shift.EmployeeId.HasValue)
            {
                var employee = await _getEmployeeQuery.QueryAsync(shift.EmployeeId.Value, cancellationToken);
                shift.EmployeeEmail = employee.Email;
            }

            return shift;
        }

        public Task<bool> AddShiftAsync(Shift shift)
        {   
            return _insertShiftQuery.QueryAsync(shift);
        }

        public async Task<bool> AssignShiftAsync(long shiftId, long employeeId, CancellationToken cancellationToken)
        {
            var employee = await _getEmployeeQuery.QueryAsync(employeeId, cancellationToken);
            if (employee == null)
            {
                return false;
            }

            if (!await ValidateShiftAsync(employeeId, shiftId))
            {
                return false;
            }

            var isAssigned = await _updateShiftQuery.QueryAsync(shiftId, employeeId);

            if (isAssigned)
            {
                await _notificationService.SendNotificationAsync(employee.Email, cancellationToken);
            }

            return isAssigned;
        }

        private async Task<bool> ValidateShiftAsync(long employeeId, long shiftId)
        {
            var shift = await _getShiftQuery.QueryAsync(shiftId);
            if (shift == null)
            {
                return false;
            }
            
            var shifts = await _allShiftsQuery.QueryAsync(employeeId);

            var isValid = shifts.All(s =>
                s.Id != shift.Id &&
                s.Start < shift.End && shift.Start < s.End);
            
            return isValid;
        }
    }    
}

