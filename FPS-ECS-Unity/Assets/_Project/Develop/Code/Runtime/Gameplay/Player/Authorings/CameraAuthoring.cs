using FpsEcs.Runtime.Gameplay.Common.Components.UnityComponentsReferences;
using FpsEcs.Runtime.Gameplay.Player.Components;
using LeoEcsLite.QoL.Authoring;
using LeoEcsLite.QoL.Utils;
using Leopotam.EcsLite;
using UnityEngine;

namespace FpsEcs.Runtime.Gameplay.Player.Authorings
{
    public class CameraAuthoring : MonoBehaviour, IAuthoring
    {
        public void Convert(EcsWorld world, int entity)
        {
            ref var camera = ref entity.Add<CameraRef>();
            camera.Value = GetComponent<Camera>();

            entity.Add<CameraInitializationNeededTag>();
        }
    }
}