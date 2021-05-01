using System.Collections.Generic;
using System.Threading.Tasks;

namespace Serverless.Notifications.Application.Common.Interfaces
{
    /// <summary>
    /// Handles Configuration table related operations.
    /// </summary>
    public interface ITableConfiguration
    {
        /// <summary>
        /// Retrieve single configuration value by providing configuration key or partition key combined.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="partitionKey"></param>
        /// <returns>The configuration value.</returns>
        Task<string> GetSettingAsync(string key, string partitionKey = null);
        
        /// <summary>
        /// Retrieve list of configuration values by providing the partition key.
        /// </summary>
        /// <param name="partitionKey"></param>
        /// <returns>A list of configuration values.</returns>
        Task<List<string>> GetAllSettingsAsync(string partitionKey);
    }
}