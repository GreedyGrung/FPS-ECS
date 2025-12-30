using FpsEcs.Runtime.Gameplay.Common.Components;
using FpsEcs.Runtime.Gameplay.Common.Components.UnityComponentsReferences;
using FpsEcs.Runtime.Gameplay.HealthFeature.Components;
using FpsEcs.Runtime.Gameplay.ProgressionFeature.Components;
using FpsEcs.Runtime.Infrastructure.Services.Pools;
using LeoEcsLite.QoL.Utils;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FpsEcs.Runtime.Gameplay.HealthFeature.Systems
{
    public class DeathSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorldInject _world;
        private readonly EcsCustomInject<IPoolsService> _poolsService;
        
        private EcsFilter _deathFilter;
        
        private EcsWorld World => _world.Value;
        private IPoolsService PoolsService => _poolsService.Value;
        
        public void Init(IEcsSystems systems)
        {
            _deathFilter = World.Inc<DeadTag, GameObjectRef>().Exc<DeathEvent>().End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _deathFilter)
            {
                if (entity.Has<PoolableObject>())
                {
                    PoolsService.ReturnToPool(entity.Get<PoolableObject>().PoolId, entity.Get<GameObjectRef>().Value);
                    entity.Get<ActorRef>().Value.Dispose();
                }
                else
                {
                    UnityEngine.Object.Destroy(entity.Get<GameObjectRef>().Value);
                }
                
                World.DelEntity(entity);
            }
        }
    }
}