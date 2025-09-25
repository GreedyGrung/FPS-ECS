using FpsEcs.Runtime.Gameplay.Common;
using FpsEcs.Runtime.Gameplay.Common.Components.UnityComponentsReferences;
using FpsEcs.Runtime.Gameplay.Player.Components;
using FpsEcs.Runtime.Infrastructure.Factories;
using FpsEcs.Runtime.Utils;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FpsEcs.Runtime.Gameplay.Player.Systems
{
    public class SpawnPlayerSystem : IEcsInitSystem
    {
        private readonly EcsWorldInject _world;
        private readonly EcsCustomInject<IGameFactory> _factory;

        private EcsFilter _playerSpawnFilter;
        
        private IGameFactory Factory => _factory.Value;
        private EcsWorld World => _world.Value;
        
        public void Init(IEcsSystems systems)
        {
            _playerSpawnFilter = World
                .Filter<PlayerSpawn>()
                .Inc<TransformRef>()
                .End();

            var entity = _playerSpawnFilter.First();
            var transform = World.GetPool<TransformRef>().Get(entity).Value;
            var playerObject = Factory.CreatePlayer(transform.position, transform.rotation);
            
            InitializePlayerEntity(playerObject);
            CreateCameraEntity(playerObject);
        }

        private void InitializePlayerEntity(GameObject playerObject)
        {
            var playerEntity = EntityFactory.CreateFrom(playerObject, World);
            
            var animatorPool = World.GetPool<AnimatorRef>();
            animatorPool.Add(playerEntity);
            ref var animator = ref animatorPool.Get(playerEntity);
            animator.Value = playerObject.GetComponentInChildren<Animator>();
        }

        private void CreateCameraEntity(GameObject playerObject)
        {
            var cameraObject = playerObject.GetComponentInChildren<Camera>().gameObject;
            EntityFactory.CreateFrom(cameraObject, World);
        }
    }
}