using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serverless.Notifications.Application.Common.Interfaces;
using Serverless.Notifications.Infrastructure.Cloud.Queues;
using Serverless.Notifications.Infrastructure.Services;

namespace Serverless.Notifications.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetSection("AzureWebJobsStorage").Value;

            services.AddScoped<INotificationPoolQueue, CloudQueueStorage>(_ =>
                new CloudQueueStorage(connectionString, "notification-pool"));

            services.AddScoped<INotificationScheduleQueue, CloudQueueStorage>(_ =>
                new CloudQueueStorage(connectionString, "scheduled-notifications"));

            services.AddScoped<ISmsQueue, CloudQueueStorage>(_ =>
                new CloudQueueStorage(connectionString, "sms"));

            services.AddScoped<IEmailQueue, CloudQueueStorage>(_ =>
                new CloudQueueStorage(connectionString, "email"));

            services.AddScoped<ITwilioSmsService, TwilioSmsService>();

            return services;
        }
    }
}
