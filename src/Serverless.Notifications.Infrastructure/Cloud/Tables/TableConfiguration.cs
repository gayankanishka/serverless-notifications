using System.Configuration;
using System.Threading.Tasks;
using Serverless.Notifications.Application.Common.Interfaces;
using Serverless.Notifications.Domain.TableEntities;

namespace Serverless.Notifications.Infrastructure.Cloud.Tables
{
    public class TableConfiguration : ITableConfiguration
    {
        private readonly ICloudStorageTable _cloudStorageTable;
        
        public TableConfiguration(ICloudStorageTable cloudStorageTable)
        {
            _cloudStorageTable = cloudStorageTable;
            _cloudStorageTable.TableName = "Configurations";
            _cloudStorageTable.PartitionKey = "Configuration";
        }
        
        public async Task<string> GetSettingAsync(string key, string partitionKey = null)
        {
            ConfigurationEntity config = await _cloudStorageTable.GetTableEntityAsync<ConfigurationEntity>(key, partitionKey);
            return config.ConfigurationValue;
        }
    }
}