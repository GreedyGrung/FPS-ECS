using FpsEcs.Runtime.Gameplay.HealthFeature.Components;
using FpsEcs.Runtime.Gameplay.MovementLogic.Components;
using FpsEcs.Runtime.Gameplay.Player.Components;
using FpsEcs.Runtime.Gameplay.ProgressionFeature.Components;
using FpsEcs.Runtime.Gameplay.Weapons.Components;
using FpsEcs.Runtime.Infrastructure.Services.SaveLoad;
using FpsEcs.Runtime.Infrastructure.Services.SaveLoad.Data;
using LeoEcsLite.QoL.Utils;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FpsEcs.Runtime.Gameplay.ProgressionFeature.Systems
{
    public class SavePlayerProgressSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorldInject _world;
        private readonly EcsCustomInject<ISaveLoadService> _saveLoadService;
        
        private EcsFilter _saveProgressFilter;
        private EcsFilter _playerFilter;
        private EcsFilter _upgradesFilter;
        private EcsFilter _playerWeaponFilter;
        
        private EcsWorld World => _world.Value;
        private ISaveLoadService SaveLoadService => _saveLoadService.Value;
        
        public void Init(IEcsSystems systems)
        {
            _saveProgressFilter = World.Inc<SaveProgressEvent>().End();
            _playerFilter = World.Inc<PlayerTag, Health, Movement>().End();
            _upgradesFilter = World.Inc<UpgradePoints, StatsUpgradeLevels>().End();
            _playerWeaponFilter = World.Inc<Weapon, WeaponInHandsTag>().End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var saveEvent in _saveProgressFilter)
            {
                PlayerProgress progress = new();
                
                foreach (var player in _playerFilter)
                {
                    progress.Health = player.Get<Health>().Value;
                    progress.Speed = player.Get<Movement>().HorizontalSpeed;
                }

                foreach (var weapon in _playerWeaponFilter)
                {
                    progress.Damage = weapon.Get<Weapon>().Damage;
                }

                foreach (var upgradesEntity in _upgradesFilter)
                {
                    var appliedUpgrades = upgradesEntity.Get<StatsUpgradeLevels>();
                    
                    progress.AvailableUpgradePoints = upgradesEntity.Get<UpgradePoints>().Value;
                    progress.HealthUpgradeLevel = appliedUpgrades.Health;
                    progress.SpeedUpgradeLevel = appliedUpgrades.Speed;
                    progress.DamageUpgradeLevel = appliedUpgrades.Damage;
                }
                
                SaveLoadService.SaveProgress(progress);
            }
        }
    }
}