using UnityEngine;
using UnityEngine.InputSystem;

namespace FpsEcs.Runtime.Infrastructure.Services.Input.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Input/Gameplay Input Map", fileName = "GameplayInputMap")]
    public class GameplayInputMap : InputMapBase
    {
        [SerializeField] private InputActionReference _moveAction;
        [SerializeField] private InputActionReference _lookAction;
        [SerializeField] private InputActionReference _attackAction;
        [SerializeField] private InputActionReference _pauseAction;
        
        public InputActionReference MoveAction => _moveAction;
        public InputActionReference LookAction => _lookAction;
        public InputActionReference AttackAction => _attackAction;
        public InputActionReference PauseAction => _pauseAction;
    }
}