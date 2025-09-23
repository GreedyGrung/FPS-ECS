using UnityEngine;
using UnityEngine.InputSystem;

namespace FpsEcs.Runtime.Infrastructure.Services.Input.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Input/Pause Input Map", fileName = "PauseInputMap")]
    public class PauseInputMap : InputMapBase
    {
        [SerializeField] private InputActionReference _pauseAction;
        
        public InputActionReference PauseAction => _pauseAction;
    }
}