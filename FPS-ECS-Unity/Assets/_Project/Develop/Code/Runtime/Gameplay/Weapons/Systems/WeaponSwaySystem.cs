using FpsEcs.Runtime.Gameplay.Common.Components.UnityComponentsReferences;
using FpsEcs.Runtime.Gameplay.Input.Components;
using FpsEcs.Runtime.Gameplay.Weapons.Components;
using LeoEcsLite.QoL.Utils;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FpsEcs.Runtime.Gameplay.Weapons.Systems
{
    public class WeaponSwaySystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorldInject _world;

        private EcsFilter _inputFilter;
        private EcsFilter _weaponFilter;

        private bool _originIsSet;
        private Vector3 _origin;
        
        private EcsWorld World => _world.Value;
        
        public void Init(IEcsSystems systems)
        {
            _inputFilter = World.Inc<PlayerInput>().End();
            _weaponFilter = World.Inc<WeaponSway, WeaponInHandsTag, TransformRef>().End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var inputEntity in _inputFilter)
            {
                var input = inputEntity.Get<PlayerInput>();

                foreach (var weaponEntity in _weaponFilter)
                {
                    var transform = weaponEntity.Get<TransformRef>().Value;
                    var sway = weaponEntity.Get<WeaponSway>();

                    if (!_originIsSet)
                    {
                        _origin = transform.localPosition;
                        _originIsSet = true;
                    }
                    
                    Vector2 clampedInput = input.Look;
                    clampedInput.x = Mathf.Clamp(clampedInput.x, -sway.Clamp, sway.Clamp);
                    clampedInput.y = Mathf.Clamp(clampedInput.y, -sway.Clamp, sway.Clamp);

                    Vector3 target = new(-clampedInput.x, -clampedInput.y, 0);
                    
                    transform.localPosition = 
                        Vector3.Lerp(transform.localPosition, target + _origin, Time.deltaTime * sway.Smoothing); 
                }
            }
        }
    }
}