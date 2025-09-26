using FpsEcs.Runtime.Gameplay.Common.Components.UnityComponentsReferences;
using Leopotam.EcsLite;
using UnityEngine;

namespace FpsEcs.Runtime.Gameplay.Common
{
    public class Actor : MonoBehaviour
    {
        private EcsWorld _world;
        private EcsPackedEntity _entity;
        
        private bool _isInitialized;
        
        public void Initialize(EcsWorld world)
        {
            if (_isInitialized)
            {
                return;
            }
            
            _world = world;
            var newEntity = _world.NewEntity();
            
            ref var transformComponent = ref world.GetPool<TransformRef>().Add(newEntity);
            transformComponent.Value = transform;

            if (gameObject.TryGetComponent(out CharacterController characterController))
            {
                ref var characterControllerComponent = ref world.GetPool<CharacterControllerRef>().Add(newEntity);
                characterControllerComponent.Value = characterController;
            }
            
            ApplyAuthorings(newEntity);
            
            _entity = _world.PackEntity(newEntity);
            _isInitialized = true;
        }

        public int GetEntity()
        {
            _entity.Unpack(_world, out var entity);
            
            return entity;
        }
        
        private void ApplyAuthorings(int entity)
        {
            foreach (var a in GetComponents<MonoBehaviour>())
            {
                if (a is IAuthoring auth && a.isActiveAndEnabled)
                {
                    auth.Convert(_world, entity);
                }
            }
        }
    }
}