using System.Reflection;
using Hospital.Application.Common.NotificationDispatchers;
using Hospital.Application.Common.NotificationDispatcherServices;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Hospital.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddOptions();
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg=>cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        services.AddSingleton<IHubStaffDispatcher, HubStaffDispatcher>();
        services.AddSignalR(options =>
        {
            options.EnableDetailedErrors = true;
        });

        return services;
    }
}