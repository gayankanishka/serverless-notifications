using Microsoft.Azure.Cosmos.Table;

namespace Serverless.Notifications.Domain.TableEntities
{
    public class ConfigurationEntity : TableEntity
    {
        public ConfigurationEntity()
        {
        }
        
        public ConfigurationEntity(string partitionKey, string rowKey)
        {
            PartitionKey = partitionKey;
            RowKey = rowKey;
        }

        public string ConfigurationValue { get; set; } = string.Empty;
    }
}