using FpsEcs.Runtime.Configs.Implementations;
using FpsEcs.Runtime.Gameplay.HealthFeature.Components;
using FpsEcs.Runtime.Gameplay.MovementLogic.Components;
using FpsEcs.Runtime.Gameplay.Player.Components;
using FpsEcs.Runtime.Gameplay.ProgressionFeature.Components;
using FpsEcs.Runtime.Gameplay.Weapons.Components;
using FpsEcs.Runtime.Infrastructure.Services.Configs;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FpsEcs.Runtime.Gameplay.ProgressionFeature.Systems
{
    public class ApplyUpgradesSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorldInject _world;
        private readonly EcsCustomInject<IConfigsProvider> _configsProvider;

        private EcsFilter _playerFilter;
        private EcsFilter _playerWeaponFilter;
        private EcsFilter _upgradePointsFilter;
        private EcsFilter _upgradeAppliedEventFilter;
        
        private EcsPool<Movement> _movementPool;
        private EcsPool<UpgradePoints> _upgradePointsPool;
        private EcsPool<StatsUpgradeLevels> _upgradeLevelsPool;
        private EcsPool<ApplyUpgradesEvent> _applyUpgradesEventPool;
        private EcsPool<Weapon> _weaponPool;
        private EcsPool<Health> _healthPool;
        
        private EcsWorld World => _world.Value;
        private GameConfig GameConfig => _configsProvider.Value.GetGameConfig();
        
        public void Init(IEcsSystems systems)
        {
            _playerFilter = World
                .Filter<PlayerTag>()
                .Inc<Movement>()
                .Inc<Health>()
                .End();

            _playerWeaponFilter = World
                .Filter<Weapon>()
                .Inc<WeaponInHandsTag>()
                .End();

            _upgradePointsFilter = World
                .Filter<UpgradePoints>()
                .Inc<StatsUpgradeLevels>()
                .End();

            _upgradeAppliedEventFilter = World
                .Filter<ApplyUpgradesEvent>()
                .End();
            
            _movementPool = World.GetPool<Movement>();
            _upgradePointsPool = World.GetPool<UpgradePoints>();
            _upgradeLevelsPool = World.GetPool<StatsUpgradeLevels>();
            _applyUpgradesEventPool = World.GetPool<ApplyUpgradesEvent>();
            _weaponPool = World.GetPool<Weapon>();
            _healthPool = World.GetPool<Health>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var eventEntity in _upgradeAppliedEventFilter)
            {
                var upgrades = _applyUpgradesEventPool.Get(eventEntity);

                var healthBonus = upgrades.Health * GameConfig.DamageBonusPerUpgradeLevel;
                var speedBonus = upgrades.Speed * GameConfig.DamageBonusPerUpgradeLevel;
                var damageBonus = upgrades.Damage * GameConfig.DamageBonusPerUpgradeLevel;
                
                ApplyUpgradesToPlayer(healthBonus, speedBonus);
                ApplyUpgradesToPlayerWeapon(damageBonus);
                HandleUpgradePointsLogic(upgrades);
            }
        }

        private void HandleUpgradePointsLogic(ApplyUpgradesEvent upgrades)
        {
            foreach (var pointsEntity in _upgradePointsFilter)
            {
                ref var points = ref _upgradePointsPool.Get(pointsEntity);
                ref var levels = ref _upgradeLevelsPool.Get(pointsEntity);
                
                var totalPoints = upgrades.Health + upgrades.Speed + upgrades.Damage;
                points.Value -= totalPoints;

                levels.Health += upgrades.Health;
                levels.Speed += upgrades.Speed;
                levels.Damage += upgrades.Damage;
            }
        }

        private void ApplyUpgradesToPlayerWeapon(float damageBonus)
        {
            foreach (var weapon in _playerWeaponFilter)
            {
                ref var stats = ref _weaponPool.Get(weapon);
                stats.Damage += damageBonus;
            }
        }

        private void ApplyUpgradesToPlayer(float healthBonus, float speedBonus)
        {
            foreach (var player in _playerFilter)
            {
                ref var health = ref _healthPool.Get(player);
                ref var movement = ref _movementPool.Get(player);
                    
                health.Value += healthBonus;
                movement.HorizontalSpeed += speedBonus;
            }
        }
    }
}