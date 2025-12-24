using FpsEcs.Runtime.Gameplay.Common.Components.UnityComponentsReferences;
using LeoEcsLite.QoL.Authoring;
using LeoEcsLite.QoL.Utils;
using Leopotam.EcsLite;
using UnityEngine;

namespace FpsEcs.Runtime.Gameplay.Common
{
    public class Actor : ActorBase
    {
        protected override void AddDefaultComponents(EcsWorld world, int entity)
        {
            base.AddDefaultComponents(world, entity);
            
            ref var transformComponent = ref entity.Add<TransformRef>();
            transformComponent.Value = transform;
            
            ref var gameObjectComponent = ref entity.Add<GameObjectRef>();
            gameObjectComponent.Value = gameObject;

            if (gameObject.TryGetComponent(out CharacterController characterController))
            {
                ref var characterControllerComponent = ref entity.Add<CharacterControllerRef>();
                characterControllerComponent.Value = characterController;
            }
        }
    }
}