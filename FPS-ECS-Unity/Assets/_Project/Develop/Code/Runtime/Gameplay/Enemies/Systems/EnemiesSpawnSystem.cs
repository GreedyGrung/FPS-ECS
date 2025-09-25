using FpsEcs.Runtime.Configs.Implementations;
using FpsEcs.Runtime.Gameplay.Common;
using FpsEcs.Runtime.Gameplay.Common.Components;
using FpsEcs.Runtime.Gameplay.Common.Components.UnityComponentsReferences;
using FpsEcs.Runtime.Gameplay.Enemies.Components;
using FpsEcs.Runtime.Infrastructure.Factories;
using FpsEcs.Runtime.Infrastructure.Services.Configs;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FpsEcs.Runtime.Gameplay.Enemies.Systems
{
    public class EnemiesSpawnSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorldInject _world;
        private readonly EcsCustomInject<IGameFactory> _factory;
        private readonly EcsCustomInject<IConfigsProvider> _configsProvider;

        private EcsFilter _enemySpawnsFilter;
        private EcsFilter _enemySpawnerRootFilter;
        private EcsFilter _enemiesFilter;

        private IGameFactory Factory => _factory.Value;
        private EcsWorld World => _world.Value;
        private GameConfig GameConfig => _configsProvider.Value.GetGameConfig();
        
        public void Init(IEcsSystems systems)
        {
            var enemySpawnerRoot = EntityFactory.Create(World);
            World.GetPool<Timer>().Add(enemySpawnerRoot);
            World.GetPool<EnemySpawnerRoot>().Add(enemySpawnerRoot);
            
            _enemySpawnsFilter = World
                .Filter<TransformRef>()
                .Inc<EnemySpawn>()
                .End();

            _enemySpawnerRootFilter = World
                .Filter<EnemySpawnerRoot>()
                .Inc<Timer>()
                .End();
            
            _enemiesFilter = World
                .Filter<Enemy>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var spawnerRoot in _enemySpawnerRootFilter)
            {
                ref var timer = ref World.GetPool<Timer>().Get(spawnerRoot);
                timer.Value += Time.deltaTime;

                if (timer.Value >= GameConfig.EnemySpawnDuration)
                {
                    if (_enemiesFilter.GetEntitiesCount() >= GameConfig.MaxEnemyCountOnLevel)
                    {
                        timer.Value = 0;
                        return;
                    }
                    
                    var enemySpawners = _enemySpawnsFilter.GetRawEntities();
                    var enemySpawn = enemySpawners[Random.Range(0, _enemySpawnsFilter.GetEntitiesCount())];
                    var spawnPoint = World.GetPool<TransformRef>().Get(enemySpawn).Value;
                    var enemyObject = Factory.CreateEnemy(spawnPoint.position, spawnPoint.rotation);
                    var enemy = EntityFactory.CreateFrom(enemyObject, World);
                    World.GetPool<EnemyInitializationNeededTag>().Add(enemy);

                    timer.Value = 0;
                }
            }
        }
    }
}