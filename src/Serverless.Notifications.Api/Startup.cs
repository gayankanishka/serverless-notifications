using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Serverless.Notifications.Application;
using Serverless.Notifications.Infrastructure;

[assembly: FunctionsStartup(typeof(Serverless.Notifications.Api.Startup))]
namespace Serverless.Notifications.Api
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            IConfiguration configuration = builder.GetContext().Configuration;

            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(configuration);
        }
    }
}
