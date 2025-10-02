using FpsEcs.Runtime.Gameplay.Enemies.Components;
using FpsEcs.Runtime.Infrastructure.Services.Configs;
using FpsEcs.Runtime.Utils;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FpsEcs.Runtime.Gameplay.Enemies.Systems
{
    public class EnemiesInitializationSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorldInject _world;
        private readonly EcsCustomInject<IConfigsProvider> _configsProvider;

        private readonly EcsPoolInject<Enemy> _enemyPool;
        private readonly EcsPoolInject<ObstacleAvoidance> _obstacleAvoidancePool;
        private readonly EcsPoolInject<EnemyInitializationNeededTag> _enemyInitializationNeededPool;
        
        private EcsFilter _enemiesInitFilter;
        
        private EcsWorld World => _world.Value;
        private IConfigsProvider ConfigsProvider => _configsProvider.Value;
        
        public void Init(IEcsSystems systems)
        {
            _enemiesInitFilter = World
                .Filter<EnemyInitializationNeededTag>()
                .Inc<Enemy>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var enemy in _enemiesInitFilter)
            {
                var id = _enemyPool.Value.Get(enemy).Id;
                var enemyConfig = ConfigsProvider.GetEnemyConfig(id);
                ref var obstacleAvoidance = ref _obstacleAvoidancePool.Value.Add(enemy);
                
                obstacleAvoidance.CheckDistance = enemyConfig.ObstacleCheckDistance;
                obstacleAvoidance.MinTurnAngle = enemyConfig.MinTurnAngle;
                obstacleAvoidance.MaxTurnAngle = enemyConfig.MaxTurnAngle;
                obstacleAvoidance.ObstacleMask = Constants.Gameplay.ObstacleLayerMask;
                
                _enemyInitializationNeededPool.Value.Del(enemy);
            }
        }
    }
}