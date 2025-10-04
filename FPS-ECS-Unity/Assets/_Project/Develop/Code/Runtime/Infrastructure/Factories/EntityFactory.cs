using FpsEcs.Runtime.Gameplay.Common;
using Leopotam.EcsLite;
using UnityEngine;

namespace FpsEcs.Runtime.Infrastructure.Factories
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

        private sealed class EntityBuilder : IEntityBuilder
        {
            private readonly EcsWorld _world;
            private readonly int _entity;

            public EntityBuilder(EcsWorld world, int entity)
            {
                _world = world;
                _entity = entity;
            }

            public IEntityBuilder With<T>() where T : struct
            {
                _world.GetPool<T>().Add(_entity);
                return this;
            }

            public IEntityBuilder With<T>(in T component) where T : struct
            {
                ref var target = ref _world.GetPool<T>().Add(_entity);
                target = component;
                return this;
            }

            public int Build() => _entity;
        }
    }
}
