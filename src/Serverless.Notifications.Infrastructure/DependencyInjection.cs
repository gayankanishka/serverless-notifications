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

            services.AddScoped<ICloudQueueStorage, CloudQueueStorage>(_ =>
                new CloudQueueStorage(connectionString));

            services.AddScoped<ITwilioSmsService, TwilioSmsService>();

            return services;
        }
    }
}
