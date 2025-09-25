using FpsEcs.Runtime.Gameplay.Common.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace FpsEcs.Runtime.Gameplay.Common.Authorings
{
    public class TimerAuthoring : MonoBehaviour, IAuthoring
    {
        public void Convert(EcsWorld world, int entity)
        {
            world.GetPool<Timer>().Add(entity);
        }
    }
}