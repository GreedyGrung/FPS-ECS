using FpsEcs.Runtime.Gameplay.Input.Components;
using LeoEcsLite.QoL.Factory;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FpsEcs.Runtime.Gameplay.Input.Systems
{
    public class InputInitializationSystem : IEcsInitSystem
    {
        private readonly EcsCustomInject<IEntityFactory> _entityFactory;
        
        public void Init(IEcsSystems systems)
        {
            _entityFactory.Value.Create()
                .With<PlayerInput>();
        }
    }
}
