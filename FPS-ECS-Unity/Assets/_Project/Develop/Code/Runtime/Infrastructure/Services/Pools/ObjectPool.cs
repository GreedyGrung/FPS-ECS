using System.Collections.Generic;
using FpsEcs.Runtime.Infrastructure.Factories;
using FpsEcs.Runtime.Utils.Enums;
using UnityEngine;

namespace FpsEcs.Runtime.Infrastructure.Services.Pools
{
    public class ObjectPool
    {
        private readonly Transform _container;
        private readonly bool _autoExpand;
        private readonly IGameFactory _gameFactory;
        private readonly GameObject _prefab;
        private readonly PoolId _poolId;
        private readonly Stack<GameObject> _pool;

        public ObjectPool(
            GameObject prefab,
            int size,
            Transform container,
            bool autoExpand,
            IGameFactory gameFactory,
            PoolId poolId)
        {
            _pool = new();
            _container = container;
            _autoExpand = autoExpand;
            _gameFactory = gameFactory;
            _poolId = poolId;
            _prefab = prefab;

            for (int i = 0; i < size; i++)
            {
                CreateItem();
            }
        }

        public GameObject Take()
        {
            if (_pool.TryPop(out var item))
            {
                if (_pool.Count == 0 && _autoExpand)
                {
                    CreateItem();
                }
                
                item.SetActive(true);

                return item;
            }

            if (_autoExpand)
            {
                CreateItem();

                var newItem = _pool.Pop();
                newItem.SetActive(true);

                return newItem;
            }

            throw new System.Exception("The pool is empty!");
        }

        public void Return(GameObject item)
        {
            item.SetActive(false);
            _pool.Push(item);
        }

        private void CreateItem(bool isActiveByDefault = false)
        {
            var item = _gameFactory.CreatePoolableObject(_prefab, _container, _poolId, isActiveByDefault);
            _pool.Push(item);
        }
    }
}