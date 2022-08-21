using Microsoft.Extensions.DependencyInjection;
using Serverless.Notifications.Application.Common.Interfaces;
using Serverless.Notifications.Application.Services;

namespace Serverless.Notifications.Application;

/// <summary>
///     Dependency injection extension to configure Application layer services.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    ///     Configure Application layer services.
    /// </summary>
    /// <param name="services"></param>
    /// <returns>The service collection.</returns>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<INotificationPoolRouter, NotificationPoolRouter>();
        services.AddScoped<IScheduleProcessor, ScheduleProcessor>();

        return services;
    }
}