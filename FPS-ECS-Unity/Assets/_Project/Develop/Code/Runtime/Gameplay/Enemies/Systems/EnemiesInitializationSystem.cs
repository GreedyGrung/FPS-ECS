using FpsEcs.Runtime.Gameplay.Enemies.Components;
using FpsEcs.Runtime.Infrastructure.Services.Configs;
using FpsEcs.Runtime.Utils;
using LeoEcsLite.QoL.Utils;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FpsEcs.Runtime.Gameplay.Enemies.Systems
{
    public class EnemiesInitializationSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorldInject _world;
        private readonly EcsCustomInject<IConfigsProvider> _configsProvider;
        
        private EcsFilter _enemiesInitFilter;
        
        private EcsWorld World => _world.Value;
        private IConfigsProvider ConfigsProvider => _configsProvider.Value;
        
        public void Init(IEcsSystems systems)
        {
            _enemiesInitFilter = World.Inc<EnemyInitializationNeededTag, Enemy>().End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var enemy in _enemiesInitFilter)
            {
                var id = enemy.Get<Enemy>().Id;
                var enemyConfig = ConfigsProvider.GetEnemyConfig(id);
                ref var obstacleAvoidance = ref enemy.Add<ObstacleAvoidance>();
                
                obstacleAvoidance.CheckDistance = enemyConfig.ObstacleCheckDistance;
                obstacleAvoidance.MinTurnAngle = enemyConfig.MinTurnAngle;
                obstacleAvoidance.MaxTurnAngle = enemyConfig.MaxTurnAngle;
                obstacleAvoidance.ObstacleMask = Constants.Gameplay.ObstacleLayerMask;
                
                enemy.Del<EnemyInitializationNeededTag>();
            }
        }
    }
}