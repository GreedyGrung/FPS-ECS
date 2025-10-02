using FpsEcs.Runtime.Gameplay.Enemies.Components;
using FpsEcs.Runtime.Gameplay.ProgressionFeature.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FpsEcs.Runtime.Gameplay.ProgressionFeature.Systems
{
    public class EnemiesDeathObserverSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorldInject _world;
        
        private readonly EcsPoolInject<UpgradePoints> _upgradePointsPool;
        private readonly EcsPoolInject<DeathEvent> _deathPool;

        private EcsFilter _enemyDeathFilter;
        private EcsFilter _upgradePointsFilter;
        
        private EcsWorld World => _world.Value;
        
        public void Init(IEcsSystems systems)
        {
            _enemyDeathFilter = World
                .Filter<DeathEvent>()
                .Inc<Enemy>()
                .End();

            _upgradePointsFilter = World
                .Filter<UpgradePoints>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var upgradePointsEntity in _upgradePointsFilter)
            {
                foreach (var diedEnemy in _enemyDeathFilter)
                {
                    ref var points = ref _upgradePointsPool.Value.Get(upgradePointsEntity).Value;
                    points++;
                    
                    _deathPool.Value.Del(diedEnemy);
                }
            }
        }
    }
}