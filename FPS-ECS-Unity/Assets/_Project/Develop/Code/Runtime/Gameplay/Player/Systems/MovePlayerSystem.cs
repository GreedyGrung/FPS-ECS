using FpsEcs.Runtime.Gameplay.Common.Components.UnityComponentsReferences;
using FpsEcs.Runtime.Gameplay.Input.Components;
using FpsEcs.Runtime.Gameplay.MovementLogic.Components;
using FpsEcs.Runtime.Gameplay.Player.Components;
using FpsEcs.Runtime.Utils;
using LeoEcsLite.QoL.Utils;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FpsEcs.Runtime.Gameplay.Player.Systems
{
    public class MovePlayerSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorldInject _world;

        private EcsFilter _inputFilter;
        private EcsFilter _playerFilter;
        
        private EcsWorld World => _world.Value;

        public void Init(IEcsSystems systems)
        {
            _playerFilter = World.Inc<TransformRef, PlayerTag, CharacterControllerRef, Movement>().End();
            _inputFilter = World.Inc<PlayerInput>().End();
        }

        public void Run(IEcsSystems systems)
        {
            float deltaTime = Time.deltaTime;

            foreach (var inputEntity in _inputFilter)
            {
                ref var input = ref inputEntity.Get<PlayerInput>();
                
                foreach (var playerEntity in _playerFilter)
                {
                    ref var body = ref playerEntity.Get<TransformRef>();
                    ref var movementRuntime = ref playerEntity.Get<Movement>();
                    var characterController = playerEntity.Get<CharacterControllerRef>().Value;

                    Vector3 forward = body.Value.forward; 
                    forward.y = 0f;
                    Vector3 right = body.Value.right; 
                    right.y = 0f;
                    forward.Normalize(); 
                    right.Normalize();

                    Vector3 movePlanar = forward * input.Move.y + right * input.Move.x;
                    
                    if (movePlanar.sqrMagnitude > 1f)
                    {
                        movePlanar.Normalize();
                    }

                    movePlanar *= movementRuntime.HorizontalSpeed;

                    if (characterController.isGrounded && movementRuntime.VerticalSpeed < 0f)
                    {
                        movementRuntime.VerticalSpeed = -2f;
                    }

                    movementRuntime.VerticalSpeed += -Mathf.Abs(Constants.Gameplay.Gravity) * deltaTime;

                    Vector3 velocity = movePlanar;
                    velocity.y = movementRuntime.VerticalSpeed;
                    
                    characterController.Move(velocity * deltaTime);
                }
            }
        }
    }
}