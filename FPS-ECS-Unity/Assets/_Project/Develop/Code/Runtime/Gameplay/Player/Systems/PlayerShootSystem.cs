using FpsEcs.Runtime.Gameplay.Common;
using FpsEcs.Runtime.Gameplay.Common.Components.UnityComponentsReferences;
using FpsEcs.Runtime.Gameplay.Input.Components;
using FpsEcs.Runtime.Gameplay.Weapons.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FpsEcs.Runtime.Gameplay.Player.Systems
{
    public class PlayerShootSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorldInject _world;
        
        private readonly EcsPoolInject<PlayerInput> _inputPool;
        private readonly EcsPoolInject<TransformRef> _transformPool;
        private readonly EcsPoolInject<Weapon> _weaponPool;
        private readonly EcsPoolInject<FireCooldown> _cooldownPool;
        private readonly EcsPoolInject<DamageEvent> _damageEventsPool;
        private readonly EcsPoolInject<FireEffect> _fireEffectPool;
        
        private EcsFilter _inputFilter;
        private EcsFilter _cameraFilter;
        private EcsFilter _weaponFilter;
        
        private EcsWorld World => _world.Value;

        public void Init(IEcsSystems systems)
        {
            _inputFilter = World
                .Filter<PlayerInput>()
                .End();

            _cameraFilter = World
                .Filter<CameraRef>()
                .Inc<TransformRef>()
                .End();
            
            _weaponFilter = World
                .Filter<Weapon>()
                .Inc<FireCooldown>()
                .Inc<FireEffect>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var inputEntity in _inputFilter)
            {
                ref var input = ref _inputPool.Value.Get(inputEntity);

                foreach (var cameraEntity in _cameraFilter)
                {
                    var transform = _transformPool.Value.Get(cameraEntity).Value;
                    
                    foreach (var weaponEntity in _weaponFilter)
                    {
                        ref var weapon = ref _weaponPool.Value.Get(weaponEntity);
                        ref var cooldown = ref _cooldownPool.Value.Get(weaponEntity);
                        var fireEffect = _fireEffectPool.Value.Get(weaponEntity).Value;
                        var now = Time.time;
                        
                        if (now < cooldown.NextTime || !input.AttackPressed)
                        {
                            continue;
                        }

                        var interval = 1f / Mathf.Max(weapon.FireRate, 0.0001f);
                        cooldown.NextTime = now + interval;

                        Vector3 origin = transform.position;
                        Vector3 dir = GetDirectionWithSpread(transform, weapon.SpreadDegrees);

                        if (Physics.Raycast(origin, dir, out var hit, weapon.MaxDistance, weapon.LayerMask, QueryTriggerInteraction.Ignore))
                        {
                            if (hit.collider.TryGetComponent(out Actor actor))
                            {
                                var entity = actor.GetEntity();
                                ref var damageEvent = ref _damageEventsPool.Value.Add(entity);
                                damageEvent.DamageAmount = weapon.Damage;
                            }
                        }
                        
                        fireEffect.Play();
                    }
                }
            }
        }
        
        private Vector3 GetDirectionWithSpread(Transform cam, float spreadDeg)
        {
            if (spreadDeg <= 0f)
            {
                return cam.forward;
            }

            float tan = Mathf.Tan(spreadDeg * Mathf.Deg2Rad);
            Vector2 jitter = Random.insideUnitCircle * tan;
            
            return (cam.forward + cam.right * jitter.x + cam.up * jitter.y).normalized;
        }
    }
}