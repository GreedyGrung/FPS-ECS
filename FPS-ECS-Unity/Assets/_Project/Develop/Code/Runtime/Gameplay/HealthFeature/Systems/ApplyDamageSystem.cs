using System;
using FpsEcs.Runtime.Gameplay.HealthFeature.Components;
using FpsEcs.Runtime.Gameplay.Weapons.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FpsEcs.Runtime.Gameplay.HealthFeature.Systems
{
    public class ApplyDamageSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorldInject _world;
        
        private EcsPool<DamageEvent> _damageEventPool;
        private EcsPool<Health> _healthPool;
        
        private EcsFilter _applyDamageFilter;
        
        private EcsWorld World => _world.Value;
        
        public void Init(IEcsSystems systems)
        {
            _applyDamageFilter = World
                .Filter<DamageEvent>()
                .Inc<Health>()
                .End();
            
            _damageEventPool = World.GetPool<DamageEvent>();
            _healthPool = World.GetPool<Health>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _applyDamageFilter)
            {
                var damage = _damageEventPool.Get(entity).DamageAmount;
                ref var health = ref _healthPool.Get(entity);
                health.Value -= damage;

                if (health.Value <= 0)
                {
                    World.GetPool<DeadTag>().Add(entity);
                }
                
                _damageEventPool.Del(entity);
            }
        }
    }
}