using FpsEcs.Runtime.Gameplay.Input.Components;
using LeoEcsLite.QoL.Utils;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FpsEcs.Runtime.Gameplay.Input.Systems
{
    public class EnableMobileInputSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorldInject _world;
        
        private EcsFilter _mobileControlsFilter;
        
        private EcsWorld World => _world.Value;
        
        public void Init(IEcsSystems systems)
        {
            _mobileControlsFilter = World.Inc<MobileControls>().End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _mobileControlsFilter)
            {
                var controls = entity.Get<MobileControls>().Value;
                
                if (controls.activeInHierarchy == false)
                {
                    controls.SetActive(ShouldEnableMobileUI());
                }
            }
        }
        
        private bool ShouldEnableMobileUI(bool forceMobileInEditor = false)
        {
#if UNITY_EDITOR
            if (forceMobileInEditor)
            {
                return true;
            }
#endif
            return Application.isMobilePlatform || SystemInfo.deviceType == DeviceType.Handheld;
        }
    }
}