using Dapper;
using Microsoft.Data.Sqlite;
using Planday.Schedule.Infrastructure.Providers.Interfaces;
using Planday.Schedule.Queries;

namespace Planday.Schedule.Infrastructure.Queries
{
    public class UpdateShiftQuery : IUpdateShiftQuery
    {
        private readonly IConnectionStringProvider _connectionStringProvider;

        public UpdateShiftQuery(IConnectionStringProvider connectionStringProvider)
        {
            _connectionStringProvider = connectionStringProvider;
        }
        
        public async Task<bool> QueryAsync(long shiftId, long employeeId)
        {
            await using var sqlConnection = new SqliteConnection(_connectionStringProvider.GetConnectionString());

            var param = new { employeeId, shiftId};
            var affectedRows = await sqlConnection.ExecuteAsync(Sql, param: param);
            
            return affectedRows == 1;
        }

        private const string Sql = @"UPDATE Shift SET EmployeeId = @employeeId WHERE Id = @shiftId;";
    }    
}

