using FpsEcs.Runtime.Gameplay.HealthFeature.Components;
using LeoEcsLite.QoL.Authoring;
using LeoEcsLite.QoL.Utils;
using Leopotam.EcsLite;
using UnityEngine;

namespace FpsEcs.Runtime.Gameplay.HealthFeature.Authorings
{
    public class HealthAuthoring : MonoBehaviour, IAuthoring
    {
        public void Convert(EcsWorld world, int entity)
        {
            entity.Add<HealthInitializationNeededTag>();
        }
    }
}