using FpsEcs.Runtime.Gameplay.Common.Components.UnityComponentsReferences;
using FpsEcs.Runtime.Gameplay.Player.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FpsEcs.Runtime.Gameplay.Player.Systems
{
    public class PlayerAnimationSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorldInject _world;
        private readonly EcsPoolInject<AnimatorRef> _animatorPool;
        private readonly EcsPoolInject<CharacterControllerRef> _characterControllerPool;
        
        private EcsFilter _playerFilter;

        public void Init(IEcsSystems systems)
        {
            _playerFilter = _world.Value
                .Filter<PlayerTag>()
                .Inc<AnimatorRef>()
                .Inc<CharacterControllerRef>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var playerEntity in _playerFilter)
            {
                var characterController = _characterControllerPool.Value.Get(playerEntity).Value;
                var animator = _animatorPool.Value.Get(playerEntity).Value;
                
                var planarVelocity = new Vector2(characterController.velocity.x, characterController.velocity.z);
                animator.SetFloat("Speed", planarVelocity.magnitude);
            }
        }
    }
}