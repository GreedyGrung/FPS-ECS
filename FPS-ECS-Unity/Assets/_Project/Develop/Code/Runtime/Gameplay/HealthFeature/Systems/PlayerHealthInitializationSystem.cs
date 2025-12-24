using FpsEcs.Runtime.Gameplay.HealthFeature.Components;
using FpsEcs.Runtime.Gameplay.Player.Components;
using FpsEcs.Runtime.Infrastructure.Services.Configs;
using LeoEcsLite.QoL.Utils;
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
            _healthPlayerInitFilter = World.Inc<HealthInitializationNeededTag, PlayerTag>().End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var player in _healthPlayerInitFilter)
            {
                ref var heath = ref player.Add<Health>();
                heath.Value = ConfigsProvider.GetPlayerConfig().Health;
                
                player.Del<HealthInitializationNeededTag>();
            }
        }
    }
}