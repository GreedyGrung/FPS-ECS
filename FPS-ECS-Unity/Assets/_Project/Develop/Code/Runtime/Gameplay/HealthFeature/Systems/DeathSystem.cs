using FpsEcs.Runtime.Gameplay.Common.Components.UnityComponentsReferences;
using FpsEcs.Runtime.Gameplay.HealthFeature.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FpsEcs.Runtime.Gameplay.HealthFeature.Systems
{
    public class DeathSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorldInject _world;
        
        private EcsPool<TransformRef> _deathPool;
        
        private EcsFilter _deathFilter;
        
        private EcsWorld World => _world.Value;
        
        public void Init(IEcsSystems systems)
        {
            _deathFilter = World
                .Filter<DeadTag>()
                .Inc<TransformRef>()
                .End();
            
            _deathPool = World.GetPool<TransformRef>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _deathFilter)
            {
                UnityEngine.Object.Destroy(_deathPool.Get(entity).Value.transform.gameObject);
                World.DelEntity(entity);
            }
        }
    }
}