using System;
using FpsEcs.Runtime.Configs.Interfaces;
using FpsEcs.Runtime.Utils;
using FpsEcs.Runtime.Utils.Enums;
using UnityEngine;

namespace FpsEcs.Runtime.Configs.Implementations
{
    [Serializable]
    public class EnemyConfig : IEntityConfig
    {
        [field: SerializeField]
        public EnemyId Id { get; set; }
        [field: SerializeField]
        public float Health { get; set; }
        [field: SerializeField]
        public float Speed { get; set; }
        [field: SerializeField]
        public int Damage { get; set; }
    }
}