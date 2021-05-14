# Serverless Notifications Using Azure Functions + Twilio + SendGrid

The solution implements a serverless notification framework. Azure functions are used to archive the serverless architecture. Twilio and sendgrid are used as the notification providers. Azure storages tables are used to store the application configurations. Azure Queue storage was used for internal comunication of the functions. One single `POST` endpoint was provided to send out the notifications `HINT: /api/notifications`.

## Highlevel architecture diagram
![alt text](https://github.com/gayankanishka/serverless-notifications/blob/refactor/docs/Serverless-Notification-V1.png?raw=true)

What's included:

- Prototype of a serverless notification framework
- Uses [`Azure Functions`](https://azure.microsoft.com/en-us/services/functions/) to implement serverless architecture
- Uses [`Queue Storage`](https://azure.microsoft.com/en-us/services/storage/queues/) to implement queue architecture
- Uses [`Table Storage`](https://azure.microsoft.com/en-us/services/storage/tables/) to store configurations
- Uses [`Twilio`](https://twilio.com) to send out SMS
- Uses [`SendGrid`](https://sendgrid.com/) to send out Emails

## Table of Content

- [Quick Start](#quick-start)
  - [Prerequisites](#prerequisites)
  - [Development Environment Setup](#development-environment-setup)
  - [Build and run](#build-and-run-from-source)
- [License](#license)

## Quick Start

After setting up your local DEV environment, you can clone this repository and run the solution. Make sure to configure the `local.settings.json` with the provided setting values. If you are using a Azure storage account replace `AzureWebJobsStorage` value with storage account connection string.

``` json
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet"
  }
}
```

### Prerequisites

You'll need the following tools:

- [Azure Service Fabric SDK](https://docs.microsoft.com/en-us/azure/service-fabric/service-fabric-get-started)
- [Azure Storage Emulator](https://docs.microsoft.com/en-us/azure/storage/common/storage-use-emulator)
- [Azure Storage Explorer](https://azure.microsoft.com/en-us/features/storage-explorer/)
- [Git](https://git-scm.com/downloads)
- [.NET](https://dotnet.microsoft.com/download), version `>=5`
- [Visual Studio](https://visualstudio.microsoft.com/), version `>=2019`
- [Twilio Trial Account](https://www.twilio.com/try-twilio)
- [SendGrid Trial Account](https://sendgrid.com/free/)

### Development Environment Setup

First clone this repository locally.

- Install all of the the prerequisite tools mentioned above.
- Connect your Azure storage account into Azure storage explore [`link`](https://docs.microsoft.com/en-us/azure-stack/user/azure-stack-storage-connect-se?view=azs-1908) Or use default emulator storage account.
- Add below Table storage configurations

### Build and run from source

With Visual studio:

Open up the solutions using Visual studio.

- Restore solution `nuget` packages.
- Rebuild solution once.
- Run the solution.
- POST [`Endpoint`](http://localhost:7071/api/notifications)

> Sample SMS notification request
``` json

{
    "body": "{\"Id\":\"ed32ef8d-1b19-40cc-915b-b8ff1b4d6ff0\",\"FromNumber\":null,\"ToNumber\":\"+NUMBER_WITH_COUNTRY_CODE\",\"MessageBody\":\"Hello there\"}",
    "notificationType": 1,
    "isScheduled": false,
    "scheduledDateTime":"2021-04-30T00:00:00"
}

```

## License

Licensed under the [MIT](LICENSE) license.
