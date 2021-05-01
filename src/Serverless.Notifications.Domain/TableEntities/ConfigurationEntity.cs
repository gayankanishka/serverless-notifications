using Microsoft.Azure.Cosmos.Table;

namespace Serverless.Notifications.Domain.TableEntities
{
    /// <summary>
    /// The table entity of the configuration table.
    /// </summary>
    public class ConfigurationEntity : TableEntity
    {
        #region Constructors

        /// <summary>
        /// Default Constructor.
        /// </summary>
        public ConfigurationEntity()
        {
        }
        
        /// <summary>
        /// Constructs with PartitionKey and RowKey.
        /// </summary>
        /// <param name="partitionKey"></param>
        /// <param name="rowKey"></param>
        public ConfigurationEntity(string partitionKey, string rowKey)
        {
            PartitionKey = partitionKey;
            RowKey = rowKey;
        }

        #endregion

        #region Public Members

        /// <summary>
        /// Get or Set the Configuration value.
        /// </summary>
        public string ConfigurationValue { get; set; } = string.Empty;

        #endregion
    }
}