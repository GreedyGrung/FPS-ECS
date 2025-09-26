using FpsEcs.Runtime.Gameplay.Common.Components;
using FpsEcs.Runtime.Gameplay.Common.Components.UnityComponentsReferences;
using FpsEcs.Runtime.Gameplay.Input.Components;
using FpsEcs.Runtime.Gameplay.Player.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FpsEcs.Runtime.Gameplay.Player.Systems
{
    public class CameraLookSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorldInject _world;

        private readonly EcsPoolInject<PlayerInput> _inputPool;
        private readonly EcsPoolInject<TransformRef> _bodyPool;
        private readonly EcsPoolInject<CameraRef> _camPool;
        private readonly EcsPoolInject<CameraState> _cameraStatePool;
        private readonly EcsPoolInject<CameraSettings> _camSettingsPool;

        private EcsFilter _inputFilter;
        private EcsFilter _cameraFilter;
        private EcsFilter _playerFilter;

        private EcsWorld World => _world.Value;
        
        public void Init(IEcsSystems systems)
        {
            _cameraFilter = World
                .Filter<CameraRef>()
                .Inc<CameraState>()
                .Inc<CameraSettings>()
                .End();

            _inputFilter = World
                .Filter<PlayerInput>()
                .End();

            _playerFilter = World
                .Filter<PlayerTag>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var inputEntity in _inputFilter)
            {
                ref var input = ref _inputPool.Value.Get(inputEntity);

                foreach (var playerEntity in _playerFilter)
                {
                    ref var body = ref _bodyPool.Value.Get(playerEntity);
                    
                    foreach (var cameraEntity in _cameraFilter)
                    {
                        ref var camera = ref _camPool.Value.Get(cameraEntity);
                        ref var cameraState = ref _cameraStatePool.Value.Get(cameraEntity);
                        ref var cameraSettings = ref _camSettingsPool.Value.Get(cameraEntity);

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