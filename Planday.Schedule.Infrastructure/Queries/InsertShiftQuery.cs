using Dapper;
using Microsoft.Data.Sqlite;
using Planday.Schedule.Infrastructure.Providers.Interfaces;
using Planday.Schedule.Queries;

namespace Planday.Schedule.Infrastructure.Queries
{
    public class InsertShiftQuery : IInsertShiftQuery
    {
        private readonly IConnectionStringProvider _connectionStringProvider;

        public InsertShiftQuery(IConnectionStringProvider connectionStringProvider)
        {
            _connectionStringProvider = connectionStringProvider;
        }
        
        public async Task<bool> QueryAsync(Shift shift)
        {
            await using var sqlConnection = new SqliteConnection(_connectionStringProvider.GetConnectionString());

            var param = new {start = shift.Start, end = shift.End};
            var affectedRows = await sqlConnection.ExecuteAsync(Sql, param: param);
            
            return affectedRows == 1;
        }

        private const string Sql = @"INSERT INTO Shift (Start, End) VALUES (@start, @end);";
    }    
}

