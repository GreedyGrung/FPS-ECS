using System;
using System.Collections.Generic;
using System.Linq;
using FpsEcs.Runtime.Utils.Enums;
using UnityEngine;

namespace FpsEcs.Runtime.Configs.ScriptableObjects
{
    [CreateAssetMenu(fileName = "PoolsConfig", menuName = "Configs/Pools")]
    public class PoolsConfig : ScriptableObject
    {
        [SerializeField] private List<PoolConfig> _items;

        public IReadOnlyList<PoolConfig> Items => _items;

        public PoolConfig Get(PoolId type) =>
            _items.SingleOrDefault(x => x.Type == type)
            ?? throw new KeyNotFoundException($"PoolsConfig item with type '{type}' not found.");
    }
    
    [Serializable]
    public class PoolConfig
    {
        public PoolId Type;
        public GameObject Prefab;
        public bool IsAutoExpandable;
        public int InitialSize;
    }
}