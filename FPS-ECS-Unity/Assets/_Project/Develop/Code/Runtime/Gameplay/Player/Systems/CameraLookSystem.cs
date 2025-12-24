using FpsEcs.Runtime.Gameplay.Common.Components.UnityComponentsReferences;
using FpsEcs.Runtime.Gameplay.Input.Components;
using FpsEcs.Runtime.Gameplay.Player.Components;
using LeoEcsLite.QoL.Utils;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FpsEcs.Runtime.Gameplay.Player.Systems
{
    public class CameraLookSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorldInject _world;

        private EcsFilter _inputFilter;
        private EcsFilter _cameraFilter;
        private EcsFilter _playerFilter;

        private EcsWorld World => _world.Value;
        
        public void Init(IEcsSystems systems)
        {
            _cameraFilter = World.Inc<CameraRef, CameraState, CameraSettings>().End();
            _inputFilter = World.Inc<PlayerInput>().End();
            _playerFilter = World.Inc<PlayerTag>().End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var inputEntity in _inputFilter)
            {
                ref var input = ref inputEntity.Get<PlayerInput>();

                foreach (var playerEntity in _playerFilter)
                {
                    ref var body = ref playerEntity.Get<TransformRef>();
                    
                    foreach (var cameraEntity in _cameraFilter)
                    {
                        ref var camera = ref cameraEntity.Get<CameraRef>();
                        ref var cameraState = ref cameraEntity.Get<CameraState>();
                        ref var cameraSettings = ref cameraEntity.Get<CameraSettings>();

                        cameraState.Yaw += input.Look.x * cameraSettings.Sensitivity;
                        cameraState.Pitch -= input.Look.y * cameraSettings.Sensitivity;
                        cameraState.Pitch = Mathf.Clamp(cameraState.Pitch, cameraSettings.MinPitch, cameraSettings.MaxPitch);

                        var bodyEuler = body.Value.localEulerAngles;
                        bodyEuler.y = cameraState.Yaw;
                        body.Value.localEulerAngles = bodyEuler;

                        camera.Value.transform.localRotation = Quaternion.Euler(cameraState.Pitch, 0f, 0f);
                    }
                }
            }
        }
    }
}