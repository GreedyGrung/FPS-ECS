using Leopotam.EcsLite;
using Leopotam.EcsLite.ExtendedSystems;

namespace LeoEcsLite.QoL.Modules
{
    public class EcsModuleBuilder
    {
        private readonly IEcsSystems _systems;

        public EcsModuleBuilder(IEcsSystems systems) => _systems = systems;

        public EcsModuleBuilder Add(IEcsSystem system)
        {
            _systems.Add(system);
            
            return this;
        }

        public EcsModuleBuilder AddRange(params IEcsSystem[] systems)
        {
            foreach (var system in systems)
            {
                _systems.Add(system);
            }

            return this;
        }

        public EcsModuleBuilder DelHere<T>() where T : struct
        {
            _systems.DelHere<T>();
            
            return this;
        }
    }
}