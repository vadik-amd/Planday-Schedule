using Dapper;
using Microsoft.Data.Sqlite;
using Planday.Schedule.Infrastructure.Providers.Interfaces;
using Planday.Schedule.Queries;

namespace Planday.Schedule.Infrastructure.Queries
{
    public class GetShiftQuery : IGetShiftQuery
    {
        private readonly IConnectionStringProvider _connectionStringProvider;

        public GetShiftQuery(IConnectionStringProvider connectionStringProvider)
        {
            _connectionStringProvider = connectionStringProvider;
        }
        
        public async Task<Shift> QueryAsync(long id)
        {
            await using var sqlConnection = new SqliteConnection(_connectionStringProvider.GetConnectionString());

            var param = new { id };
            var shiftDto = await sqlConnection.QueryFirstAsync<ShiftDto>(Sql, param: param);

            var shift = new Shift(shiftDto.Id, shiftDto.EmployeeId, DateTime.Parse(shiftDto.Start), DateTime.Parse(shiftDto.End));
        
            return shift;
        }

        private const string Sql = @"SELECT Id, EmployeeId, Start, End FROM Shift WHERE Id = @id;";
    
        private record ShiftDto(long Id, long? EmployeeId, string Start, string End);
    }    
}

