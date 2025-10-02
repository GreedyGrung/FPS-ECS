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
        
        private readonly EcsPoolInject<UpgradePoints> _upgradePointsPool;
        private readonly EcsPoolInject<StatsUpgradeLevels> _upgradeLevelPool;
        
        public void Init(IEcsSystems systems)
        {
            var progressionEntity = _entityFactory.Value.Create();
            _upgradePointsPool.Value.Add(progressionEntity);
            _upgradeLevelPool.Value.Add(progressionEntity);
        }
    }
}