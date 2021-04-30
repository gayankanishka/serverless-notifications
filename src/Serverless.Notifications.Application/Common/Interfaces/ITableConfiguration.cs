using System.Collections.Generic;
using System.Threading.Tasks;

namespace Serverless.Notifications.Application.Common.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITableConfiguration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="partitionKey"></param>
        /// <returns></returns>
        Task<string> GetSettingAsync(string key, string partitionKey = null);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="partitionKey"></param>
        /// <returns></returns>
        Task<List<string>> GetAllSettingsAsync(string partitionKey);
    }
}