using Leopotam.EcsLite;

namespace FpsEcs.Runtime.Infrastructure.Factories.Entities
{
    public class EntityBuilder : IEntityBuilder
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
    }
}