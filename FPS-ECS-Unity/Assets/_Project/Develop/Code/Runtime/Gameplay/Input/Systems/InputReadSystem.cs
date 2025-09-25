using FpsEcs.Runtime.Gameplay.Input.Components;
using FpsEcs.Runtime.Infrastructure.Services.Input;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FpsEcs.Runtime.Gameplay.Input.Systems
{
    public class InputReadSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsCustomInject<IInputService> _inputService;
        
        private EcsFilter _filter;
        private EcsPool<PlayerInput> _inputPool;
        
        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            
            _filter = world.Filter<PlayerInput>().End();
            _inputPool = world.GetPool<PlayerInput>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var input = ref _inputPool.Get(entity);
                input.Move = _inputService.Value.MoveAction.ReadValue<Vector2>();
                input.Look = _inputService.Value.LookAction.ReadValue<Vector2>();
                input.AttackPressed = _inputService.Value.AttackAction.IsPressed();
            }
        }
    }
}