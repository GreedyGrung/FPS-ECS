using FpsEcs.Runtime.Gameplay.Common;
using FpsEcs.Runtime.Gameplay.Input.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace FpsEcs.Runtime.Gameplay.Input.Authoring
{
    public class MobileControlsAuthoring : MonoBehaviour, IAuthoring
    {
        [SerializeField] private GameObject _mobileControls;
        
        public void Convert(EcsWorld world, int entity)
        {
            var pool = world.GetPool<MobileControls>();
            ref var controls = ref pool.Add(entity);
            controls.Value = _mobileControls;
        }
    }
}