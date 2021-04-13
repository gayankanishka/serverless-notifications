using Microsoft.Extensions.DependencyInjection;
using Serverless.Notifications.Application.Common.Interfaces;
using Serverless.Notifications.Application.Services;

namespace Serverless.Notifications.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<INotificationPoolRouter, NotificationPoolRouter>();
            return services;
        }
    }
}
