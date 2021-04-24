using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Table;
using Serverless.Notifications.Application.Common.Interfaces;

namespace Serverless.Notifications.Infrastructure.Cloud.Tables
{
    public class CloudStorageTable : ICloudStorageTable
    {
        private readonly CloudTableClient _cloudTableClient;

        public string TableName { get; set; }
        public string PartitionKey { get; set; }

        public CloudStorageTable(string connectionString)
        {
            CloudStorageAccount cloudStorageAccount = CreateStorageAccountFromConnectionString(connectionString);
            _cloudTableClient = cloudStorageAccount.CreateCloudTableClient();
        }
        
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
    }
}
