using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Table;

namespace Serverless.Notifications.Application.Common.Interfaces
{
    public interface ICloudStorageTable
    {
        string TableName { get; set; }
        
        public string PartitionKey { get; set; }

        Task<T> GetTableEntityAsync<T>(string rowKey, string partitionKey = null) 
            where T : TableEntity;

        Task<List<T>> GetAllTableEntitiesAsync<T>(string partitionKey = null)
            where T : TableEntity, new();
    }
}
