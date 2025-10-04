using FpsEcs.Runtime.Gameplay.Common;
using Leopotam.EcsLite;
using UnityEngine;

namespace FpsEcs.Runtime.Infrastructure.Factories.Entities
{
    public class EntityFactory : IEntityFactory
    {
        private EcsWorld _world;

        public void Initialize(EcsWorld world) => _world = world;

        public IEntityBuilder Create()
        {
            var entity = _world.NewEntity();
            
            return new EntityBuilder(_world, entity);
        }

        public int CreateFrom(GameObject gameObject)
        {
            var actor = gameObject.GetComponent<Actor>();
            actor.Initialize(_world);

            return actor.GetEntity();
        }
    }
}
