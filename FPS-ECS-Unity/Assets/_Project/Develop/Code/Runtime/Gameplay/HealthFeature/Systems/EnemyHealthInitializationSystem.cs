using FpsEcs.Runtime.Gameplay.Enemies.Components;
using FpsEcs.Runtime.Gameplay.HealthFeature.Components;
using FpsEcs.Runtime.Infrastructure.Services.Configs;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FpsEcs.Runtime.Gameplay.HealthFeature.Systems
{
    public class EnemyHealthInitializationSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorldInject _world;
        private readonly EcsCustomInject<IConfigsProvider> _configsProvider;
        
        private readonly EcsPoolInject<Health> _healthPool;
        private readonly EcsPoolInject<Enemy> _enemyPool;
        private readonly EcsPoolInject<HealthInitializationNeededTag> _healthInitializationNeededPool;
        
        private EcsFilter _healthEnemiesInitFilter;
        
        private EcsWorld World => _world.Value;
        private IConfigsProvider ConfigsProvider => _configsProvider.Value;
        
        public void Init(IEcsSystems systems)
        {
            _healthEnemiesInitFilter = World
                .Filter<HealthInitializationNeededTag>()
                .Inc<Enemy>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var enemy in _healthEnemiesInitFilter)
            {
                var healthPool = _healthPool.Value;
                healthPool.Add(enemy);
                
                ref var health = ref healthPool.Get(enemy);
                var id = _enemyPool.Value.Get(enemy).Id;
                
                var floatHealth = Random.Range(
                    ConfigsProvider.GetEnemyConfig(id).MinHealth,
                    ConfigsProvider.GetEnemyConfig(id).MaxHealth);
                health.Value = Mathf.RoundToInt(floatHealth);
                
                _healthInitializationNeededPool.Value.Del(enemy);
            }
        }
    }
}