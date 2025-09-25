using FpsEcs.Runtime.Gameplay.Common;
using FpsEcs.Runtime.Gameplay.MovementLogic.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace FpsEcs.Runtime.Gameplay.MovementLogic.Authorings
{
    public class MovementAuthoring : MonoBehaviour, IAuthoring
    {
        public void Convert(EcsWorld world, int entity)
        {
            world.GetPool<MovementInitializationNeededTag>().Add(entity);
        }
    }
}