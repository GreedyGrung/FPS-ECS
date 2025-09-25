using FpsEcs.Runtime.Gameplay.Common;
using FpsEcs.Runtime.Gameplay.HealthFeature.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace FpsEcs.Runtime.Gameplay.HealthFeature.Authorings
{
    public class HealthAuthoring : MonoBehaviour, IAuthoring
    {
        public void Convert(EcsWorld world, int entity)
        {
            world.GetPool<HealthInitializationNeededTag>().Add(entity);
        }
    }
}