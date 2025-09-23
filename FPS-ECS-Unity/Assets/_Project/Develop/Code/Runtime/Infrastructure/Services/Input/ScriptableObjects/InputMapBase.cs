using UnityEngine;
using UnityEngine.InputSystem;

namespace FpsEcs.Runtime.Infrastructure.Services.Input.ScriptableObjects
{
    public abstract class InputMapBase : ScriptableObject
    {
        [SerializeField] private InputActionAsset[] _inputActions;
        
        public InputActionAsset[] InputActions => _inputActions;

        public bool IsEnabled { get; private set; }

        public void EnableControls()
        {
            foreach (var control in _inputActions)
            {
                control.Enable();
            }

            IsEnabled = true;
        }

        public void DisableControls()
        {
            if (!IsEnabled) return;

            foreach (var control in _inputActions)
            {
                control.Disable();
            }

            IsEnabled = false;
        }
    }
}