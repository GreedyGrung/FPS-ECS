using FpsEcs.Runtime.Gameplay.Enemies.Components;
using FpsEcs.Runtime.Gameplay.UI.Components;
using LeoEcsLite.QoL.Utils;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using TMPro;

namespace FpsEcs.Runtime.Gameplay.UI.Systems
{
    public class EnemyCounterDisplaySystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorldInject _world;
        
        private EcsFilter _enemyCountFilter;
        private EcsFilter _enemyViewFilter;
        
        private EcsWorld World => _world.Value;
        
        public void Init(IEcsSystems systems)
        {
            _enemyCountFilter = World.Inc<EnemyCountComponent>().End();
            _enemyViewFilter = World.Inc<EnemyCounterViewComponent>().End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var counterEntity in _enemyCountFilter)
            {
                var enemyCount = counterEntity.Get<EnemyCountComponent>().Value;
                
                foreach (var viewEntity in _enemyViewFilter)
                {
                    var enemyView = viewEntity.Get<EnemyCounterViewComponent>().Value;
                    enemyView.text = $"Enemies on map: {enemyCount}";
                }
            }
        }
    }
}
