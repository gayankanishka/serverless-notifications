using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Table;
using Serverless.Notifications.Application.Common.Interfaces;

namespace Serverless.Notifications.Infrastructure.Cloud.Tables;

/// <inheritdoc />
public class CloudStorageTable : ICloudStorageTable
{
    #region Private Fields

    private readonly CloudTableClient _cloudTableClient;

    #endregion

    #region Constructor

    public CloudStorageTable(string connectionString)
    {
        var cloudStorageAccount = CreateStorageAccountFromConnectionString(connectionString);
        _cloudTableClient = cloudStorageAccount.CreateCloudTableClient();
    }

    #endregion

    #region Helper Methods

    private CloudStorageAccount CreateStorageAccountFromConnectionString(string storageConnectionString)
    {
        return CloudStorageAccount.Parse(storageConnectionString);
    }

    #endregion

    #region Public Members

    /// <inheritdoc />
    public string TableName { get; set; }

    /// <inheritdoc />
    public string PartitionKey { get; set; }

    #endregion

    #region Table Storage Operations

    /// <inheritdoc />
    public async Task<T> GetTableEntityAsync<T>(string rowKey, string partitionKey = null)
        where T : TableEntity
    {
        var table = _cloudTableClient.GetTableReference(TableName);

        var retrieveOperation = TableOperation.Retrieve<T>(partitionKey ?? PartitionKey, rowKey);
        var result = await table.ExecuteAsync(retrieveOperation);
        return result.Result as T;
    }

    /// <inheritdoc />
    public async Task<List<T>> GetAllTableEntitiesAsync<T>(string partitionKey = null)
        where T : TableEntity, new()
    {
        var table = _cloudTableClient.GetTableReference(TableName);

        var partitionScanQuery =
            new TableQuery<T>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal,
                partitionKey ?? PartitionKey));

        TableContinuationToken token = null;
        var entities = new List<T>();

        do
        {
            var segment = await table.ExecuteQuerySegmentedAsync(partitionScanQuery, token);
            token = segment.ContinuationToken;

            entities.AddRange(segment);
        } while (token != null);

        return entities;
    }

    #endregion
}