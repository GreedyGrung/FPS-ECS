using FpsEcs.Runtime.Gameplay.HealthFeature.Components;
using FpsEcs.Runtime.Gameplay.Player.Components;
using FpsEcs.Runtime.Gameplay.UI.Components;
using LeoEcsLite.QoL.Utils;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FpsEcs.Runtime.Gameplay.UI.Systems
{
    public class HudRedrawSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorldInject _world;
        
        private EcsFilter _playerFilter;
        private EcsFilter _healthViewFilter;
        
        private EcsWorld World => _world.Value;
        
        public void Init(IEcsSystems systems)
        {
            _playerFilter = World.Inc<PlayerTag, Health>().End();
            _healthViewFilter = World.Inc<HealthViewComponent>().End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var player in _playerFilter)
            {
                foreach (var healthViewEntity in _healthViewFilter)
                {
                    var health = player.Get<Health>().Value;
                    var healthView = healthViewEntity.Get<HealthViewComponent>().Value;
                    healthView.text = health.ToString();
                }
            }
        }
    }
}