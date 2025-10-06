using FpsEcs.Runtime.Gameplay.Common.Components.UnityComponentsReferences;
using FpsEcs.Runtime.Gameplay.Player.Components;
using FpsEcs.Runtime.Gameplay.Weapons.Authorings;
using FpsEcs.Runtime.Gameplay.Weapons.Components;
using FpsEcs.Runtime.Infrastructure.Factories;
using FpsEcs.Runtime.Infrastructure.Factories.Entities;
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
        private readonly EcsCustomInject<IEntityFactory> _entityFactory;

        private EcsFilter _playerSpawnFilter;
        
        private IGameFactory Factory => _factory.Value;
        private EcsWorld World => _world.Value;
        private IEntityFactory EntityFactory => _entityFactory.Value;
        
        public void Init(IEcsSystems systems)
        {
            _playerSpawnFilter = World
                .Filter<PlayerSpawn>()
                .Inc<TransformRef>()
                .End();

            var entity = _playerSpawnFilter.First();
            var transform = World.GetPool<TransformRef>().Get(entity).Value;
            var playerEntity = Factory.CreatePlayer(transform.position, transform.rotation);
            var playerObject = World.GetPool<GameObjectRef>().Get(playerEntity).Value;
            
            InitializePlayerEntity(ref playerEntity, playerObject);
            CreateCameraEntity(playerObject);
            CreateWeaponEntity(playerObject);
        }

        private void InitializePlayerEntity(ref int playerEntity, GameObject playerObject)
        {
            var animatorPool = World.GetPool<AnimatorRef>();
            animatorPool.Add(playerEntity);
            ref var animator = ref animatorPool.Get(playerEntity);
            animator.Value = playerObject.GetComponentInChildren<Animator>();
        }

        private void CreateCameraEntity(GameObject playerObject)
        {
            var cameraObject = playerObject.GetComponentInChildren<Camera>().gameObject;
            EntityFactory.Convert(cameraObject);
        }
        
        private void CreateWeaponEntity(GameObject playerObject)
        {
            var weaponObject = playerObject.GetComponentInChildren<WeaponAuthoring>().gameObject;
            var entity = EntityFactory.Convert(weaponObject);
            World.GetPool<WeaponInHandsTag>().Add(entity);
        }
    }
}