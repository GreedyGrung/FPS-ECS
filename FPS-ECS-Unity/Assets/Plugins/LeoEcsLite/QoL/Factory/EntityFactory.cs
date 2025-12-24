using LeoEcsLite.QoL.Authoring;
using Leopotam.EcsLite;
using UnityEngine;

namespace LeoEcsLite.QoL.Factory
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

        public int Convert(GameObject gameObject)
        {
            var actor = gameObject.GetComponent<ActorBase>();
            actor.Initialize(_world);

            return actor.GetEntity();
        }
    }
}