using FpsEcs.Runtime.Gameplay.Enemies.Components;
using FpsEcs.Runtime.Gameplay.ProgressionFeature.Components;
using LeoEcsLite.QoL.Utils;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FpsEcs.Runtime.Gameplay.ProgressionFeature.Systems
{
    public class EnemiesDeathObserverSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorldInject _world;

        private EcsFilter _enemyDeathFilter;
        private EcsFilter _upgradePointsFilter;
        
        private EcsWorld World => _world.Value;
        
        public void Init(IEcsSystems systems)
        {
            _enemyDeathFilter = World.Inc<DeathEvent, Enemy>().End();
            _upgradePointsFilter = World.Inc<UpgradePoints>().End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var upgradePointsEntity in _upgradePointsFilter)
            {
                foreach (var diedEnemy in _enemyDeathFilter)
                {
                    ref var points = ref upgradePointsEntity.Get<UpgradePoints>().Value;
                    points++;
                    
                    diedEnemy.Del<DeathEvent>();
                }
            }
        }
    }
}