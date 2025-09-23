using FpsEcs.Runtime.Gameplay.Common.Components;
using FpsEcs.Runtime.Gameplay.Input.Components;
using FpsEcs.Runtime.Gameplay.Player.Components;
using FpsEcs.Runtime.Utils;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FpsEcs.Runtime.Gameplay.Player.Systems
{
    public class MovePlayerSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorldInject _world = default;

        private readonly EcsPoolInject<PlayerInput> _inputPool = default;
        private readonly EcsPoolInject<TransformRef> _bodyPool = default;
        private readonly EcsPoolInject<CharacterControllerRef> _ccPool = default;
        private readonly EcsPoolInject<MovementInfo> _paramsPool = default;
        private readonly EcsPoolInject<MovementRuntime> _runtimePool = default;

        private EcsFilter _inputFilter;
        private EcsFilter _playerFilter;

        public void Init(IEcsSystems systems)
        {
            _playerFilter = _world.Value
                .Filter<TransformRef>()
                .Inc<CharacterControllerRef>()
                .Inc<MovementInfo>()
                .Inc<MovementRuntime>()
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
                ref var input  = ref _inputPool.Value.Get(inputEntity);
                
                foreach (var e in _playerFilter)
                {
                    ref var body   = ref _bodyPool.Value.Get(e);
                    ref var ccRef  = ref _ccPool.Value.Get(e);
                    ref var mp     = ref _paramsPool.Value.Get(e);
                    ref var mr     = ref _runtimePool.Value.Get(e);

                    var cc = ccRef.Value;

                    // Горизонтальный вектор в плоскости XZ, в локальных осях тела (учёт yaw)
                    Vector3 fwd = body.Value.forward; fwd.y = 0f;
                    Vector3 right = body.Value.right; right.y = 0f;
                    fwd.Normalize(); right.Normalize();

                    Vector3 movePlanar = (fwd * input.Move.y + right * input.Move.x);
                    if (movePlanar.sqrMagnitude > 1f) movePlanar.Normalize();
                    movePlanar *= mp.Speed;

                    // Гравитация
                    if (cc.isGrounded && mr.YVelocity < 0f)
                        mr.YVelocity = -2f; // небольшое прижатие к земле

                    mr.YVelocity += (-Mathf.Abs(Constants.Gameplay.Gravity)) * dt;

                    Vector3 velocity = movePlanar;
                    velocity.y = mr.YVelocity;

                    cc.Move(velocity * dt);
                }
            }
        }
    }
}