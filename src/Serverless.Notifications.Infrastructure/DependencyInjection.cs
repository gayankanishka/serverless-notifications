using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serverless.Notifications.Application.Common.Interfaces;
using Serverless.Notifications.Infrastructure.Cloud.Queues;
using Serverless.Notifications.Infrastructure.Cloud.Tables;
using Serverless.Notifications.Infrastructure.Services;

namespace Serverless.Notifications.Infrastructure
{
    /// <summary>
    /// Dependency injection extension to configure Infrastructure layer services.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Configure Infrastructure layer services.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetSection("AzureWebJobsStorage").Value;

            services.AddSingleton<ICloudStorageTable, CloudStorageTable>(_ =>
                new CloudStorageTable(connectionString));
            
            services.AddSingleton<ITableConfiguration, TableConfiguration>();

            services.AddScoped<ICloudQueueStorage, CloudQueueStorage>(_ =>
                new CloudQueueStorage(connectionString));

            services.AddScoped<ITwilioSmsService, TwilioSmsService>();

            return services;
        }
    }
}
