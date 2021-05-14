using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Table;
using Serverless.Notifications.Application.Common.Interfaces;

namespace Serverless.Notifications.Infrastructure.Cloud.Tables
{
    /// <inheritdoc/>
    public class CloudStorageTable : ICloudStorageTable
    {
        #region Private Fields

        private readonly CloudTableClient _cloudTableClient;

        #endregion

        #region Public Members

        /// <inheritdoc/>
        public string TableName { get; set; }
        
        /// <inheritdoc/>
        public string PartitionKey { get; set; }

        #endregion
        
        #region Constructor

        public CloudStorageTable(string connectionString)
        {
            CloudStorageAccount cloudStorageAccount = CreateStorageAccountFromConnectionString(connectionString);
            _cloudTableClient = cloudStorageAccount.CreateCloudTableClient();
        }

        #endregion
        
        #region Table Storage Operations

        /// <inheritdoc/>
        public async Task<T> GetTableEntityAsync<T>(string rowKey, string partitionKey = null)
            where T : TableEntity
        {
            try
            {
                CloudTable table = _cloudTableClient.GetTableReference(TableName);

                TableOperation retrieveOperation = TableOperation.Retrieve<T>(partitionKey ?? PartitionKey, rowKey);
                TableResult result = await table.ExecuteAsync(retrieveOperation);
                return result.Result as T;
            }
            catch (StorageException e)
            {
                throw;
            }
        }
        
        /// <inheritdoc/>
        public async Task<List<T>> GetAllTableEntitiesAsync<T>(string partitionKey = null) 
            where T : TableEntity, new()
        {
            try
            {
                CloudTable table = _cloudTableClient.GetTableReference(TableName);

                TableQuery<T> partitionScanQuery =
                    new TableQuery<T>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionKey ?? PartitionKey));

                TableContinuationToken token = null;
                List<T> entities = new List<T>();
                
                do
                {
                    TableQuerySegment<T> segment = await table.ExecuteQuerySegmentedAsync(partitionScanQuery, token);
                    token = segment.ContinuationToken;

                    entities.AddRange(segment);
                } while (token != null);

                return entities;
            }
            catch (StorageException ex)
            {
                throw;
            }
        }

        #endregion

        #region Helper Methods

        private CloudStorageAccount CreateStorageAccountFromConnectionString(string storageConnectionString)
        {
            try
            {
                return CloudStorageAccount.Parse(storageConnectionString);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}
