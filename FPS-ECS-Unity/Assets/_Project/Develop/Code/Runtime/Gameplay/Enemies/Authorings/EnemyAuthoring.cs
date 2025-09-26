using FpsEcs.Runtime.Gameplay.Common;
using FpsEcs.Runtime.Gameplay.Enemies.Components;
using FpsEcs.Runtime.Utils.Enums;
using Leopotam.EcsLite;
using UnityEngine;

namespace FpsEcs.Runtime.Gameplay.Enemies.Authorings
{
    public class EnemyAuthoring : MonoBehaviour, IAuthoring
    {
        [SerializeField] private EnemyId _id;
        
        public void Convert(EcsWorld world, int entity)
        {
            var enemiesPool = world.GetPool<Enemy>();
            enemiesPool.Add(entity);
            ref var enemy = ref enemiesPool.Get(entity);
            enemy.Id = _id;
        }
    }
}