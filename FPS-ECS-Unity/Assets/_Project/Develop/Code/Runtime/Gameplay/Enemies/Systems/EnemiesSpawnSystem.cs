using System.Collections.Generic;
using FpsEcs.Runtime.Gameplay.Enemies.Components;
using FpsEcs.Runtime.Infrastructure.Factories;
using FpsEcs.Runtime.Infrastructure.Services.Configs;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FpsEcs.Runtime.Gameplay.Enemies.Systems
{
    public class EnemiesSpawnSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorldInject _world;
        private readonly EcsCustomInject<IGameFactory> _factory;
        private readonly EcsCustomInject<LevelDataProvider> _levelDataProvider;
        private readonly EcsCustomInject<ConfigsProvider> _configsProvider;
        
        private IGameFactory Factory => _factory.Value;
        private EcsWorld World => _world.Value;
        private LevelDataProvider LevelDataProvider => _levelDataProvider.Value;
        private ConfigsProvider ConfigsProvider => _configsProvider.Value;
        
        public void Init(IEcsSystems systems)
        {
            var spawns = LevelDataProvider.EnemySpawns;

            foreach (var spawn in spawns)
            {
                var spawnEntity = World.NewEntity();
                var spawnPool = World.GetPool<EnemySpawn>();
                spawnPool.Add(spawnEntity);
            }
        }

        public void Run(IEcsSystems systems)
        {
            throw new System.NotImplementedException();
        }
    }
}