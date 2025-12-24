using FpsEcs.Runtime.Gameplay.Weapons.Components;
using FpsEcs.Runtime.Infrastructure.Services.Configs;
using FpsEcs.Runtime.Utils;
using LeoEcsLite.QoL.Utils;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FpsEcs.Runtime.Gameplay.Weapons.Systems
{
    public class WeaponInitializationSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorldInject _world;
        private readonly EcsCustomInject<IConfigsProvider> _configsProvider;
        
        private EcsFilter _weaponInitFilter;
        
        private EcsWorld World => _world.Value;
        private IConfigsProvider ConfigsProvider => _configsProvider.Value;
        
        public void Init(IEcsSystems systems)
        {
            _weaponInitFilter = World.Inc<WeaponInitializationNeededTag, Weapon, WeaponSway>().End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var weapon in _weaponInitFilter)
            {
                ref var stats = ref weapon.Get<Weapon>();
                ref var sway = ref weapon.Get<WeaponSway>();
                var weaponConfig = ConfigsProvider.GetWeaponConfig(stats.Id);
                
                stats.Damage = weaponConfig.Damage;
                stats.FireRate = weaponConfig.FireRate;
                stats.SpreadDegrees = weaponConfig.SpreadDegrees;
                stats.MaxDistance = Constants.Gameplay.FireDistance;
                stats.LayerMask = Constants.Gameplay.EnemyAndObstacleLayerMask;
                
                sway.Clamp = weaponConfig.Clamp;
                sway.Smoothing = weaponConfig.Smoothing;

                weapon.Add<FireCooldown>();
                weapon.Del<WeaponInitializationNeededTag>();
            }
        }
    }
}