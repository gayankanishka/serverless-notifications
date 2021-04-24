using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Table;

namespace Serverless.Notifications.Application.Common.Interfaces
{
    public interface ICloudStorageTable
    {
        string TableName { get; set; }

        Task<T> GetTableEntity<T>(string partitionKey, string rowKey) 
            where T : TableEntity;

        Task<List<T>> GetAllTableEntitiesAsync<T>(string partitionKey)
            where T : TableEntity, new();
    }
}
