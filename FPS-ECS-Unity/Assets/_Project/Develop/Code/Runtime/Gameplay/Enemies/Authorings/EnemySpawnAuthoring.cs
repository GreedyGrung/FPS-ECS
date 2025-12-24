using FpsEcs.Runtime.Gameplay.Enemies.Components;
using FpsEcs.Runtime.Utils.Enums;
using LeoEcsLite.QoL.Authoring;
using LeoEcsLite.QoL.Utils;
using Leopotam.EcsLite;
using UnityEngine;

namespace FpsEcs.Runtime.Gameplay.Enemies.Authorings
{
    public class EnemySpawnAuthoring : MonoBehaviour, IAuthoring
    {
        [SerializeField] private EnemyId _enemyToSpawn;
        
        public void Convert(EcsWorld world, int entity)
        {
            ref var enemySpawn = ref entity.Add<EnemySpawn>();
            enemySpawn.EnemyToSpawn = _enemyToSpawn;
        }
    }
}