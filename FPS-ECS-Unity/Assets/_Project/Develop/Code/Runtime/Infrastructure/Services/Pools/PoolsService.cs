using System;
using System.Collections.Generic;
using FpsEcs.Runtime.Infrastructure.Factories;
using FpsEcs.Runtime.Infrastructure.Services.Configs;
using FpsEcs.Runtime.Utils.Enums;

namespace FpsEcs.Runtime.Infrastructure.Services.Pools
{
    public class PoolsService : IPoolsService
    {
        private readonly IGameFactory _gameFactory;
        private readonly IConfigsProvider _configsProvider;

        private readonly Dictionary<PoolId, object> _pools = new();

        public PoolsService(IGameFactory gameFactory, IConfigsProvider configsProvider)
        {
            _gameFactory = gameFactory;
            _configsProvider = configsProvider;
        }

        public void RegisterPool(PoolId type)
        {
            if (_pools.ContainsKey(type))
            {
                throw new InvalidOperationException($"Pool with key '{type}' is already registered.");
            }

            var poolConfig = _configsProvider.GetPoolsConfig().Get(type);
            var poolName = $"[OBJECT POOL] {poolConfig.Type}";
            var parent = _gameFactory.CreateEmptyObjectWithName(poolName);
            var pool = _gameFactory.CreatePool(parent.transform, poolConfig);
            
            _pools.Add(type, pool);
        }

        public void Dispose()
        {
            _pools.Clear();
        }
    }
}