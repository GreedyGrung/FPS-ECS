using FpsEcs.Runtime.Gameplay.ProgressionFeature.Components;
using FpsEcs.Runtime.Infrastructure.Factories;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FpsEcs.Runtime.Gameplay.ProgressionFeature.Systems
{
    public class ProgressionInitializationSystem : IEcsInitSystem
    {
        private readonly EcsWorldInject _world;
        private readonly EcsCustomInject<IEntityFactory> _entityFactory;
        
        private EcsWorld World => _world.Value;
        
        public void Init(IEcsSystems systems)
        {
            var progressionEntity = _entityFactory.Value.Create();
            World.GetPool<UpgradePoints>().Add(progressionEntity);
            World.GetPool<StatsUpgradeLevels>().Add(progressionEntity);
        }
    }
}