using FpsEcs.Runtime.Gameplay.Common;
using FpsEcs.Runtime.Gameplay.Common.Components.UnityComponentsReferences;
using FpsEcs.Runtime.Gameplay.Player.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace FpsEcs.Runtime.Gameplay.Player.Authorings
{
    public class CameraAuthoring : MonoBehaviour, IAuthoring
    {
        public void Convert(EcsWorld world, int entity)
        {
            var cameraPool = world.GetPool<CameraRef>();
            cameraPool.Add(entity);

            ref var camera = ref cameraPool.Get(entity);
            camera.Value = GetComponent<Camera>();
            
            var cameraInitPool = world.GetPool<CameraInitializationNeededTag>();
            cameraInitPool.Add(entity);
        }
    }
}