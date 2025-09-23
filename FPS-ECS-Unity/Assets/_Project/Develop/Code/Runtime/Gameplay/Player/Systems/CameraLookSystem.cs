using FpsEcs.Runtime.Gameplay.Common.Components;
using FpsEcs.Runtime.Gameplay.Input.Components;
using FpsEcs.Runtime.Gameplay.Player.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FpsEcs.Runtime.Gameplay.Player.Systems
{
    public class CameraLookSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorldInject _world = default;

        private readonly EcsPoolInject<PlayerInput> _inputPool = default;
        private readonly EcsPoolInject<TransformRef> _bodyPool = default;
        private readonly EcsPoolInject<CameraRef> _camPool = default;
        private readonly EcsPoolInject<CameraState> _camStatePool = default;

        private EcsFilter _inputFilter;
        private EcsFilter _cameraFilter;

        public void Init(IEcsSystems systems)
        {
            _cameraFilter = _world.Value
                .Filter<CameraRef>()
                .Inc<CameraState>()
                .End();

            _inputFilter = _world.Value
                .Filter<PlayerInput>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            float dt = Time.deltaTime;

            foreach (var inputEntity in _inputFilter)
            {
                ref var input = ref _inputPool.Value.Get(inputEntity);
                
                foreach (var cameraEntity in _cameraFilter)
                {
                    ref var body  = ref _bodyPool.Value.Get(cameraEntity);
                    ref var cam   = ref _camPool.Value.Get(cameraEntity);
                    ref var cs    = ref _camStatePool.Value.Get(cameraEntity);

                    // аккумулируем углы (без умножения на dt — delta уже кадровая)
                    cs.Yaw   += input.Look.x * cs.Sensitivity;
                    cs.Pitch -= input.Look.y * cs.Sensitivity;

                    cs.Pitch = Mathf.Clamp(cs.Pitch, cs.MinPitch, cs.MaxPitch);

                    // применяем:
                    // тело — только Y (yaw)
                    var bodyEuler = body.Value.localEulerAngles;
                    bodyEuler.y = cs.Yaw;
                    body.Value.localEulerAngles = bodyEuler;

                    // камера — только X (pitch)
                    var camEuler = cam.Value.transform.localRotation;
                    // используем отрицание через Quaternion.Euler, чтобы избежать геймплейных "360" артефактов
                    cam.Value.transform.localRotation = Quaternion.Euler(cs.Pitch, 0f, 0f);
                }
            }
        }
    }
}