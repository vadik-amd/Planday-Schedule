using Microsoft.Extensions.DependencyInjection;
using Planday.Schedule.Domain.Services;
using Planday.Schedule.Services;

namespace Planday.Schedule.Domain.Extensions;

public static class DomainExtension
{
    public static void AddDomainServices(this IServiceCollection services)
    {
        services.AddSingleton<IShiftsService, ShiftsService>()
            .AddSingleton<INotificationService, NotificationService>();
    }
}


