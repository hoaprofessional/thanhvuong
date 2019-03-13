// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using StackExchange.Redis;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;

namespace Microsoft.AspNetCore.DataProtection
{
    /// <summary>
    /// Contains Redis-specific extension methods for modifying a <see cref="IDataProtectionBuilder"/>.
    /// </summary>
    public static class RedisDataProtectionBuilderExtensions
    {
        private const string DataProtectionKeysName = "DataProtection-Keys";

        /// <summary>
        /// Configures the data protection system to persist keys to specified key in Redis database
        /// </summary>
        /// <param name="builder">The builder instance to modify.</param>
        /// <param name="databaseFactory">The delegate used to create <see cref="IDatabase"/> instances.</param>
        /// <param name="key">The <see cref="RedisKey"/> used to store key list.</param>
        /// <returns>A reference to the <see cref="IDataProtectionBuilder" /> after this operation has completed.</returns>
        public static IDataProtectionBuilder PersistKeysToRedis(this IDataProtectionBuilder builder, Func<IDatabase> databaseFactory, RedisKey key)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            if (databaseFactory == null)
            {
                throw new ArgumentNullException(nameof(databaseFactory));
            }
            return PersistKeysToRedisInternal(builder, databaseFactory, key);
        }

        /// <summary>
        /// Configures the data protection system to persist keys to the default key ('DataProtection-Keys') in Redis database
        /// </summary>
        /// <param name="builder">The builder instance to modify.</param>
        /// <param name="connectionMultiplexer">The <see cref="IConnectionMultiplexer"/> for database access.</param>
        /// <returns>A reference to the <see cref="IDataProtectionBuilder" /> after this operation has completed.</returns>
        public static IDataProtectionBuilder PersistKeysToRedis(this IDataProtectionBuilder builder, IConnectionMultiplexer connectionMultiplexer)
        {
            return PersistKeysToRedis(builder, connectionMultiplexer, DataProtectionKeysName);
        }

        /// <summary>
        /// Configures the data protection system to persist keys to the specified key in Redis database
        /// </summary>
        /// <param name="builder">The builder instance to modify.</param>
        /// <param name="connectionMultiplexer">The <see cref="IConnectionMultiplexer"/> for database access.</param>
        /// <param name="key">The <see cref="RedisKey"/> used to store key list.</param>
        /// <returns>A reference to the <see cref="IDataProtectionBuilder" /> after this operation has completed.</returns>
        public static IDataProtectionBuilder PersistKeysToRedis(this IDataProtectionBuilder builder, IConnectionMultiplexer connectionMultiplexer, RedisKey key)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            if (connectionMultiplexer == null)
            {
                throw new ArgumentNullException(nameof(connectionMultiplexer));
            }
            return PersistKeysToRedisInternal(builder, () => connectionMultiplexer.GetDatabase(), key);
        }

        private static IDataProtectionBuilder PersistKeysToRedisInternal(IDataProtectionBuilder config, Func<IDatabase> databaseFactory, RedisKey key)
        {
            config.Services.TryAddSingleton<IXmlRepository>(services => new RedisXmlRepository(databaseFactory, key));
            return config;
        }
    }
}

namespace Microsoft.AspNetCore.DataProtection
{
    /// <summary>
    /// An XML repository backed by a Redis list entry.
    /// </summary>
    public class RedisXmlRepository : IXmlRepository
    {
        private readonly Func<IDatabase> _databaseFactory;
        private readonly RedisKey _key;

        /// <summary>
        /// Creates a <see cref="RedisXmlRepository"/> with keys stored at the given directory.
        /// </summary>
        /// <param name="databaseFactory">The delegate used to create <see cref="IDatabase"/> instances.</param>
        /// <param name="key">The <see cref="RedisKey"/> used to store key list.</param>
        public RedisXmlRepository(Func<IDatabase> databaseFactory, RedisKey key)
        {
            _databaseFactory = databaseFactory;
            _key = key;
        }

        /// <inheritdoc />
        public IReadOnlyCollection<XElement> GetAllElements()
        {
            return GetAllElementsCore().ToList().AsReadOnly();
        }

        private IEnumerable<XElement> GetAllElementsCore()
        {
            // Note: Inability to read any value is considered a fatal error (since the file may contain
            // revocation information), and we'll fail the entire operation rather than return a partial
            // set of elements. If a value contains well-formed XML but its contents are meaningless, we
            // won't fail that operation here. The caller is responsible for failing as appropriate given
            // that scenario.
            var database = _databaseFactory();
            foreach (var value in database.ListRange(_key))
            {
                yield return XElement.Parse(value);
            }
        }

        /// <inheritdoc />
        public void StoreElement(XElement element, string friendlyName)
        {
            var database = _databaseFactory();
            database.ListRightPush(_key, element.ToString(SaveOptions.DisableFormatting));
        }
        
        
    }
}
