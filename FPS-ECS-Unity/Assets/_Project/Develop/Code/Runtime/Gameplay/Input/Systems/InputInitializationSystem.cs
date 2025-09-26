using FpsEcs.Runtime.Gameplay.Input.Components;
using FpsEcs.Runtime.Infrastructure.Factories;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FpsEcs.Runtime.Gameplay.Input.Systems
{
    public class InputInitializationSystem : IEcsInitSystem
    {
        private readonly EcsPoolInject<PlayerInput> _inputPool;
        private readonly EcsCustomInject<IEntityFactory> _entityFactory;

        public void Init(IEcsSystems systems)
        {
            var inputEntity = _entityFactory.Value.Create();
            _inputPool.Value.Add(inputEntity);
        }
    }
}