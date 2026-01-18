using FpsEcs.Runtime.Gameplay.Common.Components;
using FpsEcs.Runtime.Gameplay.Common.Components.UnityComponentsReferences;
using FpsEcs.Runtime.Utils.Enums;
using LeoEcsLite.QoL.Authoring;
using LeoEcsLite.QoL.Utils;
using Leopotam.EcsLite;
using UnityEngine;

namespace FpsEcs.Runtime.Gameplay.Common
{
    public class Actor : ActorBase
    {
        private bool _isPoolable;
        private PoolId _poolId;
        
        public void MarkPoolable(PoolId poolId)
        {
            _isPoolable = true;
            _poolId = poolId;
        }

        protected override void AddDefaultComponents(EcsWorld world, int entity)
        {
            base.AddDefaultComponents(world, entity);
            
            ref var transformComponent = ref entity.Add<TransformRef>();
            transformComponent.Value = transform;
            
            ref var gameObjectComponent = ref entity.Add<GameObjectRef>();
            gameObjectComponent.Value = gameObject;

            ref var actorComponent = ref entity.Add<ActorRef>();
            actorComponent.Value = this;

            if (_isPoolable)
            {
                ref var poolableComponent = ref entity.Add<PoolableObject>();
                poolableComponent.PoolId = _poolId;
            }

            if (gameObject.TryGetComponent(out CharacterController characterController))
            {
                ref var characterControllerComponent = ref entity.Add<CharacterControllerRef>();
                characterControllerComponent.Value = characterController;
            }
        }
    }
}