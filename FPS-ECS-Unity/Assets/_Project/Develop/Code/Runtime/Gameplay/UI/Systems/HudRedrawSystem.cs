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

        private EcsFilter _playerFilter;
        private EcsFilter _healthViewFilter;

        private EcsPool<HealthViewComponent> _healthViewPool;
        private EcsPool<Health> _healthPool;
        
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
            
            _healthViewPool = World.GetPool<HealthViewComponent>();
            _healthPool = World.GetPool<Health>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var player in _playerFilter)
            {
                foreach (var healthViewEntity in _healthViewFilter)
                {
                    var health = _healthPool.Get(player).Value;
                    var healthView = _healthViewPool.Get(healthViewEntity).Value;
                    healthView.text = "Health: " + health;
                }
            }
        }
    }
}