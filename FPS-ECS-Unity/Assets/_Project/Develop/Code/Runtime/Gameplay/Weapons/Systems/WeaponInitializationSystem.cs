using FpsEcs.Runtime.Gameplay.Weapons.Components;
using FpsEcs.Runtime.Infrastructure.Services.Configs;
using FpsEcs.Runtime.Utils;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FpsEcs.Runtime.Gameplay.Weapons.Systems
{
    public class WeaponInitializationSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorldInject _world;
        private readonly EcsCustomInject<IConfigsProvider> _configsProvider;
        private readonly EcsPoolInject<Weapon> _weaponPool;
        private readonly EcsPoolInject<WeaponInitializationNeededTag> _weaponInitPool;
        
        private EcsFilter _weaponInitFilter;
        
        private EcsWorld World => _world.Value;
        private IConfigsProvider ConfigsProvider => _configsProvider.Value;
        
        public void Init(IEcsSystems systems)
        {
            _weaponInitFilter = World
                .Filter<WeaponInitializationNeededTag>()
                .Inc<Weapon>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var weapon in _weaponInitFilter)
            {
                ref var stats = ref _weaponPool.Value.Get(weapon);
                var weaponConfig = ConfigsProvider.GetWeaponConfig(stats.Id);
                stats.Damage = weaponConfig.Damage;
                stats.FireRate = weaponConfig.FireRate;
                stats.SpreadDegrees = weaponConfig.SpreadDegrees;
                stats.MaxDistance = Constants.Gameplay.FireDistance;
                stats.LayerMask = Constants.Gameplay.EnemyLayerMask;

                World.GetPool<FireCooldown>().Add(weapon);
                
                _weaponInitPool.Value.Del(weapon);
            }
        }
    }
}