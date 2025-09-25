using FpsEcs.Runtime.Gameplay.HealthFeature.Components;
using FpsEcs.Runtime.Gameplay.Player.Components;
using FpsEcs.Runtime.Infrastructure.Services.Configs;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FpsEcs.Runtime.Gameplay.HealthFeature.Systems
{
    public class PlayerHealthInitializationSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorldInject _world;
        private readonly EcsCustomInject<IConfigsProvider> _configsProvider;
        
        private EcsFilter _healthPlayerInitFilter;
        
        private EcsWorld World => _world.Value;
        private IConfigsProvider ConfigsProvider => _configsProvider.Value;
        
        public void Init(IEcsSystems systems)
        {
            _healthPlayerInitFilter = World
                .Filter<HealthInitializationNeededTag>()
                .Inc<PlayerTag>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var player in _healthPlayerInitFilter)
            {
                var healthPool = World.GetPool<Health>();
                healthPool.Add(player);
                ref var heath = ref healthPool.Get(player);
                heath.Value = ConfigsProvider.GetPlayerConfig().Health;
                
                World.GetPool<HealthInitializationNeededTag>().Del(player);
            }
        }
    }
}