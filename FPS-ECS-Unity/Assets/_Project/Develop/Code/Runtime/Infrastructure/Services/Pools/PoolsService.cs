using System;
using System.Collections.Generic;
using FpsEcs.Runtime.Infrastructure.Factories;
using FpsEcs.Runtime.Infrastructure.Services.Configs;
using FpsEcs.Runtime.Utils.Enums;
using UnityEngine;

namespace FpsEcs.Runtime.Infrastructure.Services.Pools
{
    public class PoolsService : IPoolsService
    {
        private readonly IGameFactory _gameFactory;
        private readonly IConfigsProvider _configsProvider;

        private readonly Dictionary<PoolId, ObjectPool> _pools = new();

        public PoolsService(IGameFactory gameFactory, IConfigsProvider configsProvider)
        {
            _gameFactory = gameFactory;
            _configsProvider = configsProvider;
        }

        public void RegisterPool(PoolId id)
        {
            if (_pools.ContainsKey(id))
            {
                throw new InvalidOperationException($"Pool with key '{id}' is already registered.");
            }

            var poolConfig = _configsProvider.GetPoolsConfig().Get(id);
            var poolName = $"[OBJECT POOL] {poolConfig.Type}";
            var parent = _gameFactory.CreateEmptyObjectWithName(poolName);
            var pool = _gameFactory.CreatePool(parent.transform, poolConfig);
            
            _pools.Add(id, pool);
        }

        public GameObject GetFromPool(PoolId id)
        {
            if (!_pools.TryGetValue(id, out var pool))
            {
                throw new InvalidOperationException($"Pool with key '{id}' not found.");
            }

            return pool.Take();
        }
        
        public void ReturnToPool(PoolId id, GameObject prefab)
        {
            if (!_pools.TryGetValue(id, out var pool))
            {
                throw new InvalidOperationException($"Pool with key '{id}' not found.");
            }

            pool.Return(prefab);
        }

        public void Dispose()
        {
            _pools.Clear();
        }
    }
}