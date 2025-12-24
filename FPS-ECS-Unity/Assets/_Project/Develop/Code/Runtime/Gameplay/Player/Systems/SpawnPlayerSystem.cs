using FpsEcs.Runtime.Gameplay.Common.Components.UnityComponentsReferences;
using FpsEcs.Runtime.Gameplay.Player.Components;
using FpsEcs.Runtime.Gameplay.Weapons.Authorings;
using FpsEcs.Runtime.Gameplay.Weapons.Components;
using FpsEcs.Runtime.Infrastructure.Factories;
using LeoEcsLite.QoL.Factory;
using LeoEcsLite.QoL.Utils;
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
            _playerSpawnFilter = World.Inc<PlayerSpawn, TransformRef>().End();

            var playerSpawn = _playerSpawnFilter.First();
            var spawnTransform = playerSpawn.Get<TransformRef>().Value;
            var playerEntity = Factory.CreatePlayer(spawnTransform.position, spawnTransform.rotation);
            var playerObject = playerEntity.Get<GameObjectRef>().Value;
            
            InitializePlayerEntity(ref playerEntity, playerObject);
            CreateCameraEntity(playerObject);
            CreateWeaponEntity(playerObject);
        }

        private void InitializePlayerEntity(ref int playerEntity, GameObject playerObject)
        {
            ref var animator = ref playerEntity.Add<AnimatorRef>();
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
            entity.Add<WeaponInHandsTag>();
        }
    }
}