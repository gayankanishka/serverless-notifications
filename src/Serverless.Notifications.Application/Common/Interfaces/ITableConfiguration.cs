using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Serverless.Notifications.Application.Common.Interfaces;

/// <summary>
///     Handles Configuration table related operations.
/// </summary>
public interface ITableConfiguration
{
    /// <summary>
    ///     Retrieve single configuration value by providing configuration key.
    /// </summary>
    /// <param name="key"></param>
    /// <returns>The configuration value.</returns>
    Task<string> GetSettingAsync(string key);

    /// <summary>
    ///     Retrieve single configuration value by providing a lambda expression.
    /// </summary>
    /// <param name="filter"></param>
    /// <returns>The configuration value.</returns>
    Task<string> GetSettingAsync(Func<KeyValuePair<string, string>, bool> filter);
}