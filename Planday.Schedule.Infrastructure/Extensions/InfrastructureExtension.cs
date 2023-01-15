using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Planday.Schedule.Infrastructure.Configuration.Models;
using Planday.Schedule.Infrastructure.HttpQueries;
using Planday.Schedule.Infrastructure.Providers;
using Planday.Schedule.Infrastructure.Providers.Interfaces;
using Planday.Schedule.Infrastructure.Queries;
using Planday.Schedule.Queries;

namespace Planday.Schedule.Infrastructure.Extensions;

public static class InfrastructureExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddConfiguration(configuration)
            .AddSingleton<IGetAllShiftsQuery, GetAllShiftsQuery>()
            .AddSingleton<IGetShiftQuery, GetShiftQuery>()
            .AddSingleton<IInsertShiftQuery, InsertShiftQuery>()
            .AddSingleton<IUpdateShiftQuery, UpdateShiftQuery>()
            .AddSingleton<IGetEmployeeQuery, GetEmployeeQuery>()
            .AddSingleton<IConnectionProvider, ConnectionProvider>()
            .AddSingleton<IEmailProvider, EmailProvider>();
    }

    private static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddSingleton<IConnectionStringProvider>(
                new ConnectionStringProvider(configuration.GetConnectionString("Database")))
            .Configure<HttpConfig>(configuration.GetSection("HttpConnections"))
            .Configure<EmployeeConfig>(configuration.GetSection("Employee"))
            .Configure<SmtpConfig>(configuration.GetSection("Smtp"))
            .Configure<NotificationsConfig>(configuration.GetSection("Notifications"));
    }
}