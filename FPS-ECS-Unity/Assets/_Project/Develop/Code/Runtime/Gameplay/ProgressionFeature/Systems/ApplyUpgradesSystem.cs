using FpsEcs.Runtime.Configs.Implementations;
using FpsEcs.Runtime.Gameplay.HealthFeature.Components;
using FpsEcs.Runtime.Gameplay.MovementLogic.Components;
using FpsEcs.Runtime.Gameplay.Player.Components;
using FpsEcs.Runtime.Gameplay.ProgressionFeature.Components;
using FpsEcs.Runtime.Gameplay.Weapons.Components;
using FpsEcs.Runtime.Infrastructure.Services.Configs;
using LeoEcsLite.QoL.Utils;
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
        
        private EcsWorld World => _world.Value;
        private GameConfig GameConfig => _configsProvider.Value.GetGameConfig();
        
        public void Init(IEcsSystems systems)
        {
            _playerFilter = World.Inc<PlayerTag, Movement, Health>().End();
            _playerWeaponFilter = World.Inc<Weapon, WeaponInHandsTag>().End();
            _upgradePointsFilter = World.Inc<UpgradePoints, StatsUpgradeLevels>().End();
            _upgradeAppliedEventFilter = World.Inc<ApplyUpgradesEvent>().End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var eventEntity in _upgradeAppliedEventFilter)
            {
                var upgrades = eventEntity.Get<ApplyUpgradesEvent>();

                var healthBonus = upgrades.Health * GameConfig.HealthBonusPerUpgradeLevel;
                var speedBonus = upgrades.Speed * GameConfig.SpeedBonusPerUpgradeLevel;
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
                ref var points = ref pointsEntity.Get<UpgradePoints>();
                ref var levels = ref pointsEntity.Get<StatsUpgradeLevels>();
                
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
                ref var stats = ref weapon.Get<Weapon>();
                stats.Damage += damageBonus;
            }
        }

        private void ApplyUpgradesToPlayer(float healthBonus, float speedBonus)
        {
            foreach (var player in _playerFilter)
            {
                ref var health = ref player.Get<Health>();
                ref var movement = ref player.Get<Movement>();
                    
                health.Value += healthBonus;
                movement.HorizontalSpeed += speedBonus;
            }
        }
    }
}