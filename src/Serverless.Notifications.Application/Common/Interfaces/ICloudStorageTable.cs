using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Table;

namespace Serverless.Notifications.Application.Common.Interfaces
{
    /// <summary>
    /// Handles all of the cloud table related operations.
    /// </summary>
    public interface ICloudStorageTable
    {
        /// <summary>
        /// Get or Set the table name.
        /// </summary>
        string TableName { get; set; }
        
        /// <summary>
        /// Get or Set the table partition key.
        /// </summary>
        public string PartitionKey { get; set; }

        /// <summary>
        /// Get a table entity by RowKey and PartitionKey.
        /// </summary>
        /// <param name="rowKey">The RowKey of the entity.</param>
        /// <param name="partitionKey">The PartitionKey of the entity.</param>
        /// <typeparam name="T">Type of the table entity.</typeparam>
        /// <returns>A storage table entity.</returns>
        Task<T> GetTableEntityAsync<T>(string rowKey, string partitionKey = null) 
            where T : TableEntity;

        /// <summary>
        /// Get a list of table entities by PartitionKey.
        /// </summary>
        /// <param name="partitionKey">The PartitionKey of the entity.</param>
        /// <typeparam name="T">Type of the table entity.</typeparam>
        /// <returns>A list of storage table entities.</returns>
        Task<List<T>> GetAllTableEntitiesAsync<T>(string partitionKey = null)
            where T : TableEntity, new();
    }
}
