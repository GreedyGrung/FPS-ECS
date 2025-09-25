using FpsEcs.Runtime.Gameplay.Common;
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
        
        private EcsWorld World => _world.Value;

        public void Init(IEcsSystems systems)
        {
            var inputEntity = EntityFactory.Create(World);
            _inputPool.Value.Add(inputEntity);

            Debug.Log("[ReadInputSystem] Initialized " + inputEntity);
        }
    }
}