using System.Collections.Generic;
using System.Threading.Tasks;

namespace Serverless.Notifications.Application.Common.Interfaces
{
    public interface ITableConfiguration
    {
        Task<string> GetSettingAsync(string key, string partitionKey = null);
        Task<List<string>> GetAllSettingsAsync(string partitionKey);
    }
}