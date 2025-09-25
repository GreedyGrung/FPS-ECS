using System;
using FpsEcs.Runtime.Gameplay.Enemies.Components;
using FpsEcs.Runtime.Infrastructure.Services.Configs;
using FpsEcs.Runtime.Utils;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FpsEcs.Runtime.Gameplay.Enemies.Systems
{
    public class EnemiesInitializationSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorldInject _world;
        private readonly EcsCustomInject<IConfigsProvider> _configsProvider;
        
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
                var id = World.GetPool<Enemy>().Get(enemy).Id;
                var enemyConfig = ConfigsProvider.GetEnemyConfig(id);
                ref var obstacleAvoidance = ref World.GetPool<ObstacleAvoidance>().Add(enemy);
                obstacleAvoidance.CheckDistance = enemyConfig.ObstacleCheckDistance;
                obstacleAvoidance.MinTurnAngle = enemyConfig.MinTurnAngle;
                obstacleAvoidance.MaxTurnAngle = enemyConfig.MaxTurnAngle;
                obstacleAvoidance.ObstacleMask = Constants.Gameplay.ObstacleLayerMask;
                
                World.GetPool<EnemyInitializationNeededTag>().Del(enemy);
            }
        }
    }
}