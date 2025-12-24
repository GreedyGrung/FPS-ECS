using FpsEcs.Runtime.Gameplay.MovementLogic.Components;
using LeoEcsLite.QoL.Authoring;
using LeoEcsLite.QoL.Utils;
using Leopotam.EcsLite;
using UnityEngine;

namespace FpsEcs.Runtime.Gameplay.MovementLogic.Authorings
{
    public class MovementAuthoring : MonoBehaviour, IAuthoring
    {
        public void Convert(EcsWorld world, int entity)
        {
            entity.Add<MovementInitializationNeededTag>();
        }
    }
}