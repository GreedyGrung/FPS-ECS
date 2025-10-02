using FpsEcs.Runtime.Gameplay.HealthFeature.Components;
using FpsEcs.Runtime.Gameplay.ProgressionFeature.Components;
using FpsEcs.Runtime.Gameplay.Weapons.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FpsEcs.Runtime.Gameplay.HealthFeature.Systems
{
    public class ApplyDamageSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorldInject _world;
        
        private EcsPoolInject<DamageEvent> _damageEventPool;
        private EcsPoolInject<Health> _healthPool;
        private EcsPoolInject<DeadTag> _deadPool;
        private EcsPoolInject<DeathEvent> _deathEventPool;
        
        private EcsFilter _applyDamageFilter;
        
        private EcsWorld World => _world.Value;
        
        public void Init(IEcsSystems systems)
        {
            _applyDamageFilter = World
                .Filter<DamageEvent>()
                .Inc<Health>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _applyDamageFilter)
            {
                var damage = _damageEventPool.Value.Get(entity).DamageAmount;
                ref var health = ref _healthPool.Value.Get(entity);
                health.Value -= damage;

                if (health.Value <= 0)
                {
                    _deadPool.Value.Add(entity);
                    _deathEventPool.Value.Add(entity);
                }
                
                _damageEventPool.Value.Del(entity);
            }
        }
    }
}