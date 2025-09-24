using FpsEcs.Runtime.Gameplay.Common.Components.UnityComponentsReferences;
using Leopotam.EcsLite;
using UnityEngine;

namespace FpsEcs.Runtime.Gameplay.Helpers
{
    public static class EntityFactory
    {
        public static int Create(EcsWorld world)
        {
            return world.NewEntity();
        }

        public static int CreateFrom(GameObject gameObject, EcsWorld world)
        {
            int entity = world.NewEntity();

            ref var transform = ref world.GetPool<TransformRef>().Add(entity);
            transform.Value = gameObject.transform;

            if (gameObject.TryGetComponent(out CharacterController characterController))
            {
                ref var characterControllerComponent = ref world.GetPool<CharacterControllerRef>().Add(entity);
                characterControllerComponent.Value = characterController;
            }
            
            ApplyAuthorings(gameObject, entity, world);
            
            return entity;
        }

        private static void ApplyAuthorings(GameObject go, int entity, EcsWorld world)
        {
            foreach (var a in go.GetComponents<MonoBehaviour>())
            {
                if (a is IAuthoring auth && a.isActiveAndEnabled)
                {
                    auth.Convert(world, entity);
                }
            }
        }
    }
}