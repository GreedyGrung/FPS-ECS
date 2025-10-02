using FpsEcs.Runtime.Gameplay.HealthFeature.Components;
using FpsEcs.Runtime.Gameplay.Player.Components;
using FpsEcs.Runtime.Gameplay.UI.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FpsEcs.Runtime.Gameplay.UI.Systems
{
    public class HudRedrawSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorldInject _world;
        
        private readonly EcsPoolInject<HealthViewComponent> _healthViewPool;
        private readonly EcsPoolInject<Health> _healthPool;
        
        private EcsFilter _playerFilter;
        private EcsFilter _healthViewFilter;
        
        private EcsWorld World => _world.Value;
        
        public void Init(IEcsSystems systems)
        {
            _playerFilter = World
                .Filter<PlayerTag>()
                .Inc<Health>()
                .End();

            _healthViewFilter = World
                .Filter<HealthViewComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var player in _playerFilter)
            {
                foreach (var healthViewEntity in _healthViewFilter)
                {
                    var health = _healthPool.Value.Get(player).Value;
                    var healthView = _healthViewPool.Value.Get(healthViewEntity).Value;
                    healthView.text = health.ToString();
                }
            }
        }
    }
}