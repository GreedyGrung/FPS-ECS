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
                    progress.Health = World.GetPool<Health>().Get(player).Value;
                    progress.Speed = World.GetPool<Movement>().Get(player).HorizontalSpeed;
                }

                foreach (var weapon in _playerWeaponFilter)
                {
                    progress.Damage = World.GetPool<Weapon>().Get(weapon).Damage;
                }

                foreach (var upgrades in _upgradesFilter)
                {
                    progress.AvailableUpgradePoints = World.GetPool<UpgradePoints>().Get(upgrades).Value;
                    var statsPool = World.GetPool<StatsUpgradeLevels>();
                    progress.HealthUpgradeLevel = statsPool.Get(upgrades).Health;
                    progress.SpeedUpgradeLevel = statsPool.Get(upgrades).Speed;
                    progress.DamageUpgradeLevel = statsPool.Get(upgrades).Damage;
                }
                
                SaveLoadService.SaveProgress(progress);
            }
        }
    }
}