using FpsEcs.Runtime.Gameplay.HealthFeature.Components;
using FpsEcs.Runtime.Gameplay.ProgressionFeature.Components;
using FpsEcs.Runtime.Gameplay.Weapons.Components;
using LeoEcsLite.QoL.Utils;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FpsEcs.Runtime.Gameplay.HealthFeature.Systems
{
    public class ApplyDamageSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorldInject _world;
        
        private EcsFilter _applyDamageFilter;
        
        private EcsWorld World => _world.Value;
        
        public void Init(IEcsSystems systems)
        {
            _applyDamageFilter = World.Inc<Health, DamageEvent>().End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _applyDamageFilter)
            {
                var damage = entity.Get<DamageEvent>().DamageAmount;
                ref var health = ref entity.Get<Health>();
                health.Value -= damage;

                if (health.Value <= 0)
                {
                    entity.Add<DeadTag>();
                    entity.Add<DeathEvent>();
                }
                
                entity.Del<DamageEvent>();
            }
        }
    }
}