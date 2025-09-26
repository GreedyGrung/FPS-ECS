using FpsEcs.Runtime.Gameplay.Common;
using Leopotam.EcsLite;
using UnityEngine;

namespace FpsEcs.Runtime.Infrastructure.Factories
{
    public class EntityFactory : IEntityFactory
    {
        private EcsWorld _world;
        
        public EcsWorld World => _world;
        
        public void Initialize(EcsWorld world) => _world = world;
        
        public int Create() => _world.NewEntity();

        public int CreateFrom(GameObject gameObject)
        {
            var actor = gameObject.GetComponent<Actor>();
            actor.Initialize(_world);
            
            return actor.GetEntity();
        }
    }
}