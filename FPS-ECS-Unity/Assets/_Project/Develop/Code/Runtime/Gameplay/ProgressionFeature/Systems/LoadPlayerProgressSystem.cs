using FpsEcs.Runtime.Gameplay.HealthFeature.Components;
using FpsEcs.Runtime.Gameplay.MovementLogic.Components;
using FpsEcs.Runtime.Gameplay.Player.Components;
using FpsEcs.Runtime.Gameplay.ProgressionFeature.Components;
using FpsEcs.Runtime.Gameplay.Weapons.Components;
using FpsEcs.Runtime.Infrastructure.Services.SaveLoad;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FpsEcs.Runtime.Gameplay.ProgressionFeature.Systems
{
    public class LoadPlayerProgressSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorldInject _world;
        private readonly EcsCustomInject<ISaveLoadService> _saveLoadService;
        
        private EcsFilter _progressLoadingNeededFilter;
        private EcsFilter _playerFilter;
        private EcsFilter _upgradesFilter;
        private EcsFilter _playerWeaponFilter;
        
        private EcsWorld World => _world.Value;
        private ISaveLoadService SaveLoadService => _saveLoadService.Value;
        
        public void Init(IEcsSystems systems)
        {
            World.GetPool<ProgressLoadingNeededTag>().Add(World.NewEntity());

            _progressLoadingNeededFilter = World
                .Filter<ProgressLoadingNeededTag>()
                .End();
            
            _playerFilter = World
                .Filter<PlayerTag>()
                .Inc<Health>()
                .Inc<Movement>()
                .End();

            _upgradesFilter = World
                .Filter<UpgradePoints>()
                .Inc<StatsUpgradeLevels>()
                .Exc<ProgressLoadingNeededTag>()
                .End();

            _playerWeaponFilter = World
                .Filter<Weapon>()
                .Inc<WeaponInHandsTag>()
                .Exc<ProgressLoadingNeededTag>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var loadingNeeded in _progressLoadingNeededFilter)
            {
                var progress = SaveLoadService.LoadProgress();

                if (progress == null)
                {
                    World.GetPool<ProgressLoadingNeededTag>().Del(loadingNeeded);
                    return;
                }

                foreach (var player in _playerFilter)
                {
                    ref var health = ref World.GetPool<Health>().Get(player);
                    health.Value = progress.Health;
                    
                    ref var movement = ref World.GetPool<Movement>().Get(player);
                    movement.HorizontalSpeed = progress.Speed;
                }

                foreach (var weapon in _playerWeaponFilter)
                {
                    ref var stats = ref World.GetPool<Weapon>().Get(weapon);
                    stats.Damage = progress.Damage;
                }

                foreach (var upgradesEntity in _upgradesFilter)
                {
                    ref var upgrades = ref World.GetPool<StatsUpgradeLevels>().Get(upgradesEntity);
                    upgrades.Damage = progress.DamageUpgradeLevel;
                    upgrades.Health = progress.HealthUpgradeLevel;
                    upgrades.Speed = progress.SpeedUpgradeLevel;
                
                    ref var points = ref World.GetPool<UpgradePoints>().Get(upgradesEntity);
                    points.Value = progress.AvailableUpgradePoints;
                }

                World.GetPool<ProgressLoadingNeededTag>().Del(loadingNeeded);
            }
        }
    }
}