using FpsEcs.Runtime.Gameplay.Common;
using FpsEcs.Runtime.Gameplay.Common.Components.UnityComponentsReferences;
using FpsEcs.Runtime.Gameplay.Input.Components;
using FpsEcs.Runtime.Gameplay.Weapons.Components;
using LeoEcsLite.QoL.Utils;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FpsEcs.Runtime.Gameplay.Player.Systems
{
    public class PlayerShootSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorldInject _world;
        
        private EcsFilter _inputFilter;
        private EcsFilter _cameraFilter;
        private EcsFilter _weaponFilter;
        
        private EcsWorld World => _world.Value;

        public void Init(IEcsSystems systems)
        {
            _inputFilter = World.Inc<PlayerInput>().End();
            _cameraFilter = World.Inc<CameraRef, TransformRef>().End();
            _weaponFilter = World.Inc<Weapon, FireCooldown, FireEffect>().End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var inputEntity in _inputFilter)
            {
                ref var input = ref inputEntity.Get<PlayerInput>();

                foreach (var cameraEntity in _cameraFilter)
                {
                    var transform = cameraEntity.Get<TransformRef>().Value;
                    
                    foreach (var weaponEntity in _weaponFilter)
                    {
                        ref var weapon = ref weaponEntity.Get<Weapon>();
                        ref var cooldown = ref weaponEntity.Get<FireCooldown>();
                        var fireEffect = weaponEntity.Get<FireEffect>().Value;
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
                                ref var damageEvent = ref entity.Add<DamageEvent>();
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