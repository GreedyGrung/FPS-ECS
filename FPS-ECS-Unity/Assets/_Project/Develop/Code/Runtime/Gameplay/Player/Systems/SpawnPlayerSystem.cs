using FpsEcs.Runtime.Gameplay.Common.Components;
using FpsEcs.Runtime.Gameplay.Player.Components;
using FpsEcs.Runtime.Infrastructure.Factories;
using FpsEcs.Runtime.Infrastructure.Services.Configs;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FpsEcs.Runtime.Gameplay.Player.Systems
{
    public class SpawnPlayerSystem : IEcsInitSystem
    {
        private readonly EcsWorldInject _world;
        private readonly EcsCustomInject<IGameFactory> _factory;
        private readonly EcsCustomInject<LevelDataProvider> _levelDataProvider;
        private readonly EcsCustomInject<ConfigsProvider> _configsProvider;
        
        private IGameFactory Factory => _factory.Value;
        private EcsWorld World => _world.Value;
        private LevelDataProvider LevelDataProvider => _levelDataProvider.Value;
        private ConfigsProvider ConfigsProvider => _configsProvider.Value;
        
        public void Init(IEcsSystems systems)
        {
            var playerObject = Factory.CreatePlayer(
                LevelDataProvider.PlayerSpawn.position, 
                LevelDataProvider.PlayerSpawn.rotation);
            
            var playerEntity = World.NewEntity();

            var playerPool = World.GetPool<PlayerTag>();
            playerPool.Add(playerEntity);
            
            var movementInfoPool = World.GetPool<MovementInfo>();
            movementInfoPool.Add(playerEntity);
            ref var movementInfoComponent = ref movementInfoPool.Get(playerEntity);
            movementInfoComponent.Speed = ConfigsProvider.GetPlayerConfig().Speed;
            
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
            
            var cameraEntity = World.NewEntity();
            
            var cameraPool = World.GetPool<CameraRef>();
            cameraPool.Add(cameraEntity);
            ref var camera = ref cameraPool.Get(cameraEntity);
            camera.Value = playerObject.GetComponentInChildren<Camera>();
            
            var cameraStatePool = World.GetPool<CameraState>();
            cameraStatePool.Add(cameraEntity);
            ref var cameraState = ref cameraStatePool.Get(cameraEntity);
            cameraState.Sensitivity = 2;
            cameraState.MinPitch = -90;
            cameraState.MaxPitch = 90;
        }
    }
}