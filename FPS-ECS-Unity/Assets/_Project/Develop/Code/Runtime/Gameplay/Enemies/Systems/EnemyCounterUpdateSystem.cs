using FpsEcs.Runtime.Gameplay.Enemies.Components;
using LeoEcsLite.QoL.Factory;
using LeoEcsLite.QoL.Utils;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FpsEcs.Runtime.Gameplay.Enemies.Systems
{
    public class EnemyCounterUpdateSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorldInject _world;
        private readonly EcsCustomInject<EntityFactory> _entityFactory;
        
        private EcsFilter _enemyFilter;
        private EcsFilter _enemyCountFilter;
        
        private EcsWorld World => _world.Value;
        private EntityFactory EntityFactory => _entityFactory.Value;
        
        public void Init(IEcsSystems systems)
        {
            EntityFactory.Create().With<EnemyCountComponent>();
            
            _enemyFilter = World.Inc<Enemy>().End();
            _enemyCountFilter = World.Inc<EnemyCountComponent>().End();
        }

        public void Run(IEcsSystems systems)
        {
            int enemyCount = 0;
            
            foreach (var enemy in _enemyFilter)
            {
                enemyCount++;
            }

            foreach (var counterEntity in _enemyCountFilter)
            {
                ref var countComponent = ref counterEntity.Get<EnemyCountComponent>();
                countComponent.Value = enemyCount;
            }
        }
    }
}
