using FpsEcs.Runtime.Gameplay.Enemies.Components;
using FpsEcs.Runtime.Utils.Enums;
using LeoEcsLite.QoL.Authoring;
using LeoEcsLite.QoL.Utils;
using Leopotam.EcsLite;
using UnityEngine;

namespace FpsEcs.Runtime.Gameplay.Enemies.Authorings
{
    public class EnemyAuthoring : MonoBehaviour, IAuthoring
    {
        [SerializeField] private EnemyId _id;
        
        public void Convert(EcsWorld world, int entity)
        {
            ref var enemy = ref entity.Add<Enemy>();
            enemy.Id = _id;
        }
    }
}