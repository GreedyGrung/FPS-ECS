using FpsEcs.Runtime.Gameplay.Common;
using FpsEcs.Runtime.Gameplay.Enemies.Components;
using FpsEcs.Runtime.Utils.Enums;
using Leopotam.EcsLite;
using UnityEngine;

namespace FpsEcs.Runtime.Gameplay.Enemies.Authorings
{
    public class EnemySpawnAuthoring : MonoBehaviour, IAuthoring
    {
        [SerializeField] private EnemyId _enemyToSpawn;
        
        public void Convert(EcsWorld world, int entity)
        {
            var enemySpawnPool = world.GetPool<EnemySpawn>();
            enemySpawnPool.Add(entity);
            ref var enemySpawn = ref enemySpawnPool.Get(entity);
            enemySpawn.EnemyToSpawn = _enemyToSpawn;
        }
    }
}