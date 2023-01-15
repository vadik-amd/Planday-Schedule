using Microsoft.Extensions.Options;
using Planday.Schedule.Infrastructure.Configuration.Models;
using Planday.Schedule.Infrastructure.Providers.Interfaces;
using Planday.Schedule.Queries;

namespace Planday.Schedule.Infrastructure.HttpQueries
{
    public class GetEmployeeQuery : IGetEmployeeQuery
    {
        private readonly IConnectionProvider _connectionProvider;
        private readonly IOptions<EmployeeConfig> _options;

        public GetEmployeeQuery(IConnectionProvider connectionProvider, IOptions<EmployeeConfig> options)
        {
            _connectionProvider = connectionProvider;
            _options = options;
        }

        public async Task<Employee> QueryAsync(long id, CancellationToken cancellationToken)
        {
            var path = $"{_options.Value.GetPath}/{id}";
            return await _connectionProvider.GetAsync<Employee>(path, cancellationToken);
        }
    }
}

