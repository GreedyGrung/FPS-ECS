using FpsEcs.Runtime.Gameplay.Player.Components;
using LeoEcsLite.QoL.Authoring;
using LeoEcsLite.QoL.Utils;
using Leopotam.EcsLite;
using UnityEngine;

namespace FpsEcs.Runtime.Gameplay.Player.Authorings
{
    public class PlayerSpawnAuthoring : MonoBehaviour, IAuthoring
    {
        public void Convert(EcsWorld world, int entity)
        {
            entity.Add<PlayerSpawn>();
        }
    }
}