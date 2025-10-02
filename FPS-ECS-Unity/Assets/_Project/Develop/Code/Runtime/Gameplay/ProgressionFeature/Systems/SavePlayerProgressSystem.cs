using FpsEcs.Runtime.Gameplay.HealthFeature.Components;
using FpsEcs.Runtime.Gameplay.MovementLogic.Components;
using FpsEcs.Runtime.Gameplay.Player.Components;
using FpsEcs.Runtime.Gameplay.ProgressionFeature.Components;
using FpsEcs.Runtime.Gameplay.Weapons.Components;
using FpsEcs.Runtime.Infrastructure.Services.SaveLoad;
using FpsEcs.Runtime.Infrastructure.Services.SaveLoad.Data;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FpsEcs.Runtime.Gameplay.ProgressionFeature.Systems
{
    public class SavePlayerProgressSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorldInject _world;
        private readonly EcsCustomInject<ISaveLoadService> _saveLoadService;
        
        private readonly EcsPoolInject<ProgressLoadingNeededTag> _progressLoadingNeededPool;
        private readonly EcsPoolInject<Health> _healthPool;
        private readonly EcsPoolInject<Movement> _movementPool;
        private readonly EcsPoolInject<Weapon> _weaponPool;
        private readonly EcsPoolInject<StatsUpgradeLevels> _upgradeLevelPool;
        private readonly EcsPoolInject<UpgradePoints> _upgradePointsPool;
        
        private EcsFilter _saveProgressFilter;
        private EcsFilter _playerFilter;
        private EcsFilter _upgradesFilter;
        private EcsFilter _playerWeaponFilter;
        
        private EcsWorld World => _world.Value;
        private ISaveLoadService SaveLoadService => _saveLoadService.Value;
        
        public void Init(IEcsSystems systems)
        {
            _saveProgressFilter = World
                .Filter<SaveProgressEvent>()
                .End();

            _playerFilter = World
                .Filter<PlayerTag>()
                .Inc<Health>()
                .Inc<Movement>()
                .End();

            _upgradesFilter = World
                .Filter<UpgradePoints>()
                .Inc<StatsUpgradeLevels>()
                .End();

            _playerWeaponFilter = World
                .Filter<Weapon>()
                .Inc<WeaponInHandsTag>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var saveEvent in _saveProgressFilter)
            {
                PlayerProgress progress = new();
                
                foreach (var player in _playerFilter)
                {
                    progress.Health = _healthPool.Value.Get(player).Value;
                    progress.Speed = _movementPool.Value.Get(player).HorizontalSpeed;
                }

                foreach (var weapon in _playerWeaponFilter)
                {
                    progress.Damage = _weaponPool.Value.Get(weapon).Damage;
                }

                foreach (var upgrades in _upgradesFilter)
                {
                    progress.AvailableUpgradePoints = _upgradePointsPool.Value.Get(upgrades).Value;
                    var upgradeLevelPool = _upgradeLevelPool.Value;
                    progress.HealthUpgradeLevel = upgradeLevelPool.Get(upgrades).Health;
                    progress.SpeedUpgradeLevel = upgradeLevelPool.Get(upgrades).Speed;
                    progress.DamageUpgradeLevel = upgradeLevelPool.Get(upgrades).Damage;
                }
                
                SaveLoadService.SaveProgress(progress);
            }
        }
    }
}