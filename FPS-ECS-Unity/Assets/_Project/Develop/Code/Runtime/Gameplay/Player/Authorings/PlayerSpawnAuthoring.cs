using FpsEcs.Runtime.Gameplay.Common;
using FpsEcs.Runtime.Gameplay.Player.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace FpsEcs.Runtime.Gameplay.Player.Authorings
{
    public class PlayerSpawnAuthoring : MonoBehaviour, IAuthoring
    {
        public void Convert(EcsWorld world, int entity)
        {
            var playerSpawnPool = world.GetPool<PlayerSpawn>();
            playerSpawnPool.Add(entity);
        }
    }
}