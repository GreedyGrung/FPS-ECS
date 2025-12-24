using FpsEcs.Runtime.Gameplay.Input.Components;
using LeoEcsLite.QoL.Authoring;
using LeoEcsLite.QoL.Utils;
using Leopotam.EcsLite;
using UnityEngine;

namespace FpsEcs.Runtime.Gameplay.Input.Authoring
{
    public class MobileControlsAuthoring : MonoBehaviour, IAuthoring
    {
        [SerializeField] private GameObject _mobileControls;
        
        public void Convert(EcsWorld world, int entity)
        {
            ref var controls = ref entity.Add<MobileControls>();
            controls.Value = _mobileControls;
        }
    }
}