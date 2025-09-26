using FpsEcs.Runtime.Gameplay.ProgressionFeature.Components;
using FpsEcs.Runtime.Utils;
using Leopotam.EcsLite;
using UnityEngine;

namespace FpsEcs.Runtime.Infrastructure.Services.Upgrades
{
    public class UpgradesService : IUpgradesService
    {
        private EcsWorld _world;

        public int AvailablePoints => GetAvailablePoints();

        public void Initialize(EcsWorld world)
        {
            _world = world;
        }

        public void Apply(UpgradesPoints upgrades)
        {
            var entity = _world.NewEntity();
            ref var upgradeEvent = ref _world.GetPool<ApplyUpgradesEvent>().Add(entity);
            
            upgradeEvent.Health = upgrades.Health;
            upgradeEvent.Speed = upgrades.Speed;
            upgradeEvent.Damage = upgrades.Damage;
        }
        
        private int GetAvailablePoints()
        {
            if (_world == null)
            {
                return 0;
            }
            
            var entity = _world.Filter<UpgradePoints>().End().First();
            var pool = _world.GetPool<UpgradePoints>();

            return pool.Get(entity).Value;
        }
    }
}