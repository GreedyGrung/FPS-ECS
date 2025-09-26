using System;
using FpsEcs.Runtime.Gameplay.Common;
using FpsEcs.Runtime.Gameplay.ProgressionFeature.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FpsEcs.Runtime.Gameplay.ProgressionFeature.Systems
{
    public class ProgressionInitializationSystem : IEcsInitSystem
    {
        private readonly EcsWorldInject _world;
        
        private EcsWorld World => _world.Value;
        
        public void Init(IEcsSystems systems)
        {
            var progressionEntity = EntityFactory.Create(World);
            World.GetPool<UpgradePoints>().Add(progressionEntity);
            World.GetPool<StatsUpgradeLevels>().Add(progressionEntity);
        }
    }
}