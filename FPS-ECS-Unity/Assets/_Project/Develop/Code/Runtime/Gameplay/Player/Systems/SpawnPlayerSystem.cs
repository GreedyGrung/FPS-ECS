using FpsEcs.Runtime.Gameplay.Common.Components;
using FpsEcs.Runtime.Gameplay.Player.Components;
using FpsEcs.Runtime.Infrastructure.Factories;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FpsEcs.Runtime.Gameplay.Player.Systems
{
    public class SpawnPlayerSystem : IEcsInitSystem
    {
        private readonly EcsWorldInject _world;
        private readonly EcsCustomInject<IGameFactory> _factory;
        
        private IGameFactory Factory => _factory.Value;
        private EcsWorld World => _world.Value;
        
        public void Init(IEcsSystems systems)
        {
            var playerObject = Factory.CreatePlayer(new Vector3(10, 10, 10));
            
            var playerEntity = World.NewEntity();

            var playerPool = World.GetPool<PlayerTag>();
            playerPool.Add(playerEntity);
            
            var movementInfoPool = World.GetPool<MovementInfo>();
            movementInfoPool.Add(playerEntity);
            
            var movementRuntimePool = World.GetPool<MovementRuntime>();
            movementRuntimePool.Add(playerEntity);

            var transformPool = World.GetPool<TransformRef>();
            transformPool.Add(playerEntity);
            ref var transformComponent = ref transformPool.Get(playerEntity);
            transformComponent.Value = playerObject.transform;
            
            var characterControllerPool = World.GetPool<CharacterControllerRef>();
            characterControllerPool.Add(playerEntity);
            ref var characterController = ref characterControllerPool.Get(playerEntity);
            characterController.Value = playerObject.GetComponent<CharacterController>();
            
            var cameraPool = World.GetPool<CameraRef>();
            cameraPool.Add(playerEntity);
            ref var camera = ref cameraPool.Get(playerEntity);
            camera.Value = playerObject.GetComponentInChildren<Camera>();
            
            var cameraStatePool = World.GetPool<CameraState>();
            cameraStatePool.Add(playerEntity);
            
            ref var cameraState = ref cameraStatePool.Get(playerEntity);
            cameraState.Sensitivity = 2;
            cameraState.MinPitch = -90;
            cameraState.MaxPitch = 90;
        }
    }
}