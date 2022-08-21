using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Serverless.Notifications.Application;
using Serverless.Notifications.AzureFunctions;
using Serverless.Notifications.Infrastructure;

[assembly: FunctionsStartup(typeof(Startup))]

namespace Serverless.Notifications.AzureFunctions;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        var configuration = builder.GetContext().Configuration;

        builder.Services.AddApplication();
        builder.Services.AddInfrastructure(configuration);
    }
}