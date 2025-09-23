using FpsEcs.Runtime.Gameplay.Input.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FpsEcs.Runtime.Gameplay.Input.Systems
{
    public class InputInitializationSystem : IEcsInitSystem
    {
        private readonly EcsWorldInject _world;
        private readonly EcsPoolInject<PlayerInput> _inputPool = default;

        public void Init(IEcsSystems systems)
        {
            var world = _world.Value;

            var inputEntity = world.NewEntity();
            _inputPool.Value.Add(inputEntity);

            Debug.Log("[ReadInputSystem] Initialized");
        }
    }
}