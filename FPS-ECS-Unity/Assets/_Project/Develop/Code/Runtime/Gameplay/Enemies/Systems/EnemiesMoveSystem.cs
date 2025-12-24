using FpsEcs.Runtime.Gameplay.Common.Components;
using FpsEcs.Runtime.Gameplay.Common.Components.UnityComponentsReferences;
using FpsEcs.Runtime.Gameplay.Enemies.Components;
using FpsEcs.Runtime.Gameplay.MovementLogic.Components;
using FpsEcs.Runtime.Utils;
using LeoEcsLite.QoL.Utils;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FpsEcs.Runtime.Gameplay.Enemies.Systems
{
    public class EnemiesMoveSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorldInject _world;
        private EcsFilter _enemiesFilter;
        
        private EcsWorld World => _world.Value;
        
        public void Init(IEcsSystems systems)
        {
            _enemiesFilter = World
                .Inc<Movement, TransformRef, CharacterControllerRef, ObstacleAvoidance, RaycastPoint>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            float deltaTime = Time.deltaTime;

            foreach (var entity in _enemiesFilter)
            {
                ref var movement = ref entity.Get<Movement>();
                ref var transform = ref entity.Get<TransformRef>().Value;
                ref var characterController = ref entity.Get<CharacterControllerRef>().Value;
                ref var av = ref entity.Get<ObstacleAvoidance>();
                var raycastPoint = entity.Get<RaycastPoint>().Value.position;

                Vector3 horizontal = transform.forward * movement.HorizontalSpeed;

                Vector3 fwd = transform.forward;
                fwd.y = 0f;
                fwd.Normalize();
                bool hit = Physics.Raycast(raycastPoint, fwd, out _,
                    av.CheckDistance, av.ObstacleMask,
                    QueryTriggerInteraction.Ignore);

                if (hit)
                {
                    float angleAbs = Random.Range(av.MinTurnAngle, av.MaxTurnAngle);
                    float sign = Random.value < 0.5f ? 1f : -1f;
                    transform.Rotate(0f, angleAbs * sign, 0f, Space.World);
                }

                if (characterController.isGrounded)
                {
                    movement.VerticalSpeed = -2f;
                }
                else
                {
                    movement.VerticalSpeed -= Constants.Gameplay.Gravity * deltaTime;
                }

                Vector3 velocity = horizontal;
                velocity += Vector3.up * movement.VerticalSpeed;

                characterController.Move(velocity * deltaTime);
                
                Debug.DrawLine(raycastPoint, raycastPoint + fwd * av.CheckDistance, hit ? Color.red : Color.green);
            }
        }
    }
}