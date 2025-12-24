using FpsEcs.Runtime.Configs.Implementations;
using FpsEcs.Runtime.Gameplay.Common.Components;
using FpsEcs.Runtime.Gameplay.Common.Components.UnityComponentsReferences;
using FpsEcs.Runtime.Gameplay.Enemies.Components;
using FpsEcs.Runtime.Infrastructure.Factories;
using FpsEcs.Runtime.Infrastructure.Services.Configs;
using LeoEcsLite.QoL.Factory;
using LeoEcsLite.QoL.Utils;
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
        private readonly EcsCustomInject<IEntityFactory> _entityFactory;
        
        private EcsFilter _enemySpawnsFilter;
        private EcsFilter _enemySpawnerRootFilter;
        private EcsFilter _enemiesFilter;

        private IGameFactory Factory => _factory.Value;
        private EcsWorld World => _world.Value;
        private GameConfig GameConfig => _configsProvider.Value.GetGameConfig();
        private IEntityFactory EntityFactory => _entityFactory.Value;
        
        public void Init(IEcsSystems systems)
        {
            EntityFactory.Create().With<Timer>().With<EnemySpawnerRoot>();

            _enemySpawnsFilter = World.Inc<TransformRef, EnemySpawn>().End();
            _enemySpawnerRootFilter = World.Inc<EnemySpawnerRoot, Timer>().End();
            _enemiesFilter = World.Inc<Enemy>().End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var spawnerRoot in _enemySpawnerRootFilter)
            {
                ref var timer = ref spawnerRoot.Get<Timer>();
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
                    var spawnPoint = enemySpawn.Get<TransformRef>().Value;
                    var enemy = Factory.CreateEnemy(spawnPoint.position, spawnPoint.rotation);

                    enemy.Add<EnemyInitializationNeededTag>();

                    timer.Value = 0;
                }
            }
        }
    }
}
