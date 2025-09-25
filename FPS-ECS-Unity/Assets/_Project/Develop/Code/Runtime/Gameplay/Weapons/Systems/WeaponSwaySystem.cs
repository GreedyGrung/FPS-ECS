using FpsEcs.Runtime.Gameplay.Common.Components.UnityComponentsReferences;
using FpsEcs.Runtime.Gameplay.Input.Components;
using FpsEcs.Runtime.Gameplay.Weapons.Components;
using FpsEcs.Runtime.Infrastructure.Services.Configs;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FpsEcs.Runtime.Gameplay.Weapons.Systems
{
    public class WeaponSwaySystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorldInject _world;

        private readonly EcsPoolInject<PlayerInput> _inputPool;
        private readonly EcsPoolInject<TransformRef> _transformPool;
        private readonly EcsPoolInject<WeaponSway> _weaponSwayPool;

        private EcsFilter _inputFilter;
        private EcsFilter _weaponFilter;

        private Vector3 _origin;
        
        private EcsWorld World => _world.Value;
        
        public void Init(IEcsSystems systems)
        {
            _inputFilter = World
                .Filter<PlayerInput>()
                .End();

            _weaponFilter = World
                .Filter<WeaponSway>()
                .Inc<WeaponInHandsTag>()
                .Inc<TransformRef>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var inputEntity in _inputFilter)
            {
                var input = _inputPool.Value.Get(inputEntity);

                foreach (var weaponEntity in _weaponFilter)
                {
                    var transform = _transformPool.Value.Get(weaponEntity).Value;
                    var sway =  _weaponSwayPool.Value.Get(weaponEntity);

                    if (_origin == Vector3.zero)
                    {
                        _origin = transform.localPosition;
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