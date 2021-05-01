using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serverless.Notifications.Application.Common.Interfaces;
using Serverless.Notifications.Domain.TableEntities;

namespace Serverless.Notifications.Infrastructure.Cloud.Tables
{
    /// <inheritdoc/>
    public class TableConfiguration : ITableConfiguration
    {
        #region Private Fields

        private readonly ICloudStorageTable _cloudStorageTable;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructs with DI.
        /// </summary>
        /// <param name="cloudStorageTable"></param>
        public TableConfiguration(ICloudStorageTable cloudStorageTable)
        {
            _cloudStorageTable = cloudStorageTable;
            _cloudStorageTable.TableName = "Configurations";
            _cloudStorageTable.PartitionKey = "Configuration";
        }

        #endregion

        #region Table Configuration Operations.

        /// <inheritdoc/>
        public async Task<string> GetSettingAsync(string key, string partitionKey = null)
        {
            ConfigurationEntity config = await _cloudStorageTable
                .GetTableEntityAsync<ConfigurationEntity>(key, partitionKey);
            
            return config?.ConfigurationValue;
        }

        /// <inheritdoc/>
        public async Task<List<string>> GetAllSettingsAsync(string partitionKey)
        {
            List<ConfigurationEntity> entities = await _cloudStorageTable
                .GetAllTableEntitiesAsync<ConfigurationEntity>(partitionKey);

            return entities.Select(val => val.ConfigurationValue).ToList();
        }

        #endregion
    }
}