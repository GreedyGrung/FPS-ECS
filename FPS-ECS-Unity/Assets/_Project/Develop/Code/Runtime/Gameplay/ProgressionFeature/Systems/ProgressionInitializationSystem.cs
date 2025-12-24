using FpsEcs.Runtime.Gameplay.ProgressionFeature.Components;
using LeoEcsLite.QoL.Factory;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FpsEcs.Runtime.Gameplay.ProgressionFeature.Systems
{
    public class ProgressionInitializationSystem : IEcsInitSystem
    {
        private readonly EcsCustomInject<IEntityFactory> _entityFactory;

        public void Init(IEcsSystems systems)
        {
            _entityFactory.Value.Create()
                .With<UpgradePoints>()
                .With<StatsUpgradeLevels>();
        }
    }
}
