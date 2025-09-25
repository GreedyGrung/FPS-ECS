using FpsEcs.Runtime.Gameplay.Common.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace FpsEcs.Runtime.Gameplay.Common.Authorings
{
    public class RaycastPointAuthoring : MonoBehaviour, IAuthoring
    {
        [SerializeField] private Transform _point;
        
        public void Convert(EcsWorld world, int entity)
        {
            ref var point = ref world.GetPool<RaycastPoint>().Add(entity);
            point.Value = _point;
        }
    }
}