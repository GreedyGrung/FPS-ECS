using FpsEcs.Runtime.Gameplay.Common.Components;
using LeoEcsLite.QoL.Authoring;
using LeoEcsLite.QoL.Utils;
using Leopotam.EcsLite;
using UnityEngine;

namespace FpsEcs.Runtime.Gameplay.Common.Authorings
{
    public class RaycastPointAuthoring : MonoBehaviour, IAuthoring
    {
        [SerializeField] private Transform _point;
        
        public void Convert(EcsWorld world, int entity)
        {
            ref var point = ref entity.Add<RaycastPoint>();
            point.Value = _point;
        }
    }
}