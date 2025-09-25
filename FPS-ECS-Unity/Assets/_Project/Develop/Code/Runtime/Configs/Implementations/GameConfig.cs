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
    }
}