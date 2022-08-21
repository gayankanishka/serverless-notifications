using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serverless.Notifications.Application.Common.Interfaces;
using Serverless.Notifications.Domain.TableEntities;

namespace Serverless.Notifications.Infrastructure.Services;

/// <inheritdoc />
public class TableConfiguration : ITableConfiguration
{
    #region Constructor

    public TableConfiguration(ICloudStorageTable cloudStorageTable)
    {
        _cloudStorageTable = cloudStorageTable;
        _cloudStorageTable.TableName = TABLE_NAME;
        _cloudStorageTable.PartitionKey = PARTITION_KEY;

        _configurationCache = new Dictionary<string, string>();
    }

    #endregion

    #region Helper Methods

    private async Task UpdateCacheIfExpired()
    {
        var isCacheExpired = _lastUpdatedDateTime is null ||
                             DateTime.UtcNow >= _lastUpdatedDateTime?.AddMinutes(REFRESH_INTERVAL_MINUTES);

        if (isCacheExpired)
        {
            var entities =
                await _cloudStorageTable.GetAllTableEntitiesAsync<ConfigurationEntity>();

            _configurationCache =
                entities.ToDictionary(_ => _.RowKey, _ => _.ConfigurationValue);

            _lastUpdatedDateTime = DateTime.UtcNow;
        }
    }

    #endregion

    #region Private Fields

    private const string TABLE_NAME = "Configurations";
    private const string PARTITION_KEY = "Configuration";
    private const int REFRESH_INTERVAL_MINUTES = 15;

    private readonly ICloudStorageTable _cloudStorageTable;

    private IReadOnlyDictionary<string, string> _configurationCache;
    private DateTime? _lastUpdatedDateTime;

    #endregion

    #region Configuration Operation

    /// <inheritdoc />
    public async Task<string> GetSettingAsync(string key)
    {
        await UpdateCacheIfExpired();

        return _configurationCache.TryGetValue(key, out var setting)
            ? setting
            : string.Empty;
    }

    /// <inheritdoc />
    public async Task<string> GetSettingAsync(Func<KeyValuePair<string, string>, bool> filter)
    {
        await UpdateCacheIfExpired();
        return _configurationCache.FirstOrDefault(filter).Value;
    }

    #endregion
}