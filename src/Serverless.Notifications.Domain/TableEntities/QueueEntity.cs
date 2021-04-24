using Microsoft.Azure.Cosmos.Table;

namespace Serverless.Notifications.Domain.TableEntities
{
    public class QueueEntity : TableEntity
    {
        public QueueEntity()
        {
        }
        
        public QueueEntity(string rowKey)
        {
            PartitionKey = "Queue";
            RowKey = rowKey;
        }

        public string ConfigurationValue { get; set; }
    }
}