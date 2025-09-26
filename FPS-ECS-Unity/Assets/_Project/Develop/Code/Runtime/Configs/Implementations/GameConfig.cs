using System;
using FpsEcs.Runtime.Configs.Interfaces;
using UnityEngine;

namespace FpsEcs.Runtime.Configs.Implementations
{
    [Serializable]
    public class GameConfig : IConfig 
    {
        [field: SerializeField]
        public float EnemySpawnDuration { get; set; }
        [field: SerializeField]
        public int MaxEnemyCountOnLevel { get; set; }
        [field: SerializeField]
        public float HealthBonusPerUpgradeLevel { get; set; }
        [field: SerializeField]
        public int HealthBonusLimit { get; set; }
        [field: SerializeField]
        public float SpeedBonusPerUpgradeLevel { get; set; }
        [field: SerializeField]
        public int SpeedBonusLimit { get; set; }
        [field: SerializeField]
        public float DamageBonusPerUpgradeLevel { get; set; }
        [field: SerializeField]
        public int DamageBonusLimit { get; set; }
        
    }
}