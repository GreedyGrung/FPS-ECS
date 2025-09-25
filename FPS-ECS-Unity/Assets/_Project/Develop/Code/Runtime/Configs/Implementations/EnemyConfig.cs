using System;
using FpsEcs.Runtime.Utils.Enums;
using UnityEngine;

namespace FpsEcs.Runtime.Configs.Implementations
{
    [Serializable]
    public class EnemyConfig
    {
        [field: SerializeField]
        public EnemyId Id { get; set; }
        [field: SerializeField]
        public float MinHealth { get; set; }
        [field: SerializeField]
        public float MaxHealth { get; set; }
        [field: SerializeField]
        public float Speed { get; set; }
    }
}