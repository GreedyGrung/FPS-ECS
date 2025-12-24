using FpsEcs.Runtime.Gameplay.HealthFeature.Components;
using FpsEcs.Runtime.Gameplay.MovementLogic.Components;
using FpsEcs.Runtime.Gameplay.Player.Components;
using FpsEcs.Runtime.Gameplay.ProgressionFeature.Components;
using FpsEcs.Runtime.Gameplay.Weapons.Components;
using FpsEcs.Runtime.Infrastructure.Services.SaveLoad;
using LeoEcsLite.QoL.Factory;
using LeoEcsLite.QoL.Utils;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FpsEcs.Runtime.Gameplay.ProgressionFeature.Systems
{
    public class LoadPlayerProgressSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorldInject _world;
        private readonly EcsCustomInject<ISaveLoadService> _saveLoadService;
        private readonly EcsCustomInject<IEntityFactory> _entityFactory;
        
        private EcsFilter _progressLoadingNeededFilter;
        private EcsFilter _playerFilter;
        private EcsFilter _upgradesFilter;
        private EcsFilter _playerWeaponFilter;
        
        private EcsWorld World => _world.Value;
        private ISaveLoadService SaveLoadService => _saveLoadService.Value;
        private IEntityFactory EntityFactory => _entityFactory.Value;
        
        public void Init(IEcsSystems systems)
        {
            EntityFactory.Create().With<ProgressLoadingNeededTag>();

            _progressLoadingNeededFilter = World.Inc<ProgressLoadingNeededTag>().End();
            _playerFilter = World.Inc<PlayerTag, Health, Movement>().End();
            _upgradesFilter = World
                .Inc<UpgradePoints, StatsUpgradeLevels>()
                .Exc<ProgressLoadingNeededTag>().End();
            _playerWeaponFilter = World
                .Inc<Weapon, WeaponInHandsTag>()
                .Exc<ProgressLoadingNeededTag>().End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var loadingNeeded in _progressLoadingNeededFilter)
            {
                var progress = SaveLoadService.LoadProgress();

                foreach (var player in _playerFilter)
                {
                    ref var health = ref player.Get<Health>();
                    health.Value = progress.Health;
                    
                    ref var movement = ref player.Get<Movement>();
                    movement.HorizontalSpeed = progress.Speed;
                }

                foreach (var weapon in _playerWeaponFilter)
                {
                    ref var stats = ref weapon.Get<Weapon>();
                    stats.Damage = progress.Damage;
                }

                foreach (var upgradesEntity in _upgradesFilter)
                {
                    ref var upgrades = ref upgradesEntity.Get<StatsUpgradeLevels>();
                    upgrades.Damage = progress.DamageUpgradeLevel;
                    upgrades.Health = progress.HealthUpgradeLevel;
                    upgrades.Speed = progress.SpeedUpgradeLevel;
                
                    ref var points = ref upgradesEntity.Get<UpgradePoints>();
                    points.Value = progress.AvailableUpgradePoints;
                }

                loadingNeeded.Del<ProgressLoadingNeededTag>();
            }
        }
    }
}