using System;
using FpsEcs.Runtime.Infrastructure.Services.Input.ScriptableObjects;
using UnityEngine.InputSystem;

namespace FpsEcs.Runtime.Infrastructure.Services.Input
{
    public class MobileInputService : IInputService
    {
        public int InputX { get; }
        public int InputY { get; }
        
        public bool AttackInput { get; }

        public event Action OnPauseInput;
        public GameplayInputMap GameplayInputMap { get; }
        public PauseInputMap PauseInputMap { get; }
        public InputAction MoveAction { get; }
        public InputAction LookAction { get; }
        public InputAction AttackAction { get; }
        public bool PauseActionThisFrame { get; }
        public void SwitchInputMaps()
        {
            throw new NotImplementedException();
        }

        public InputAction PauseAction { get; }
        public void SetGameplayInputEnabled(bool enable)
        {
            throw new NotImplementedException();
        }

        public void SetPauseInputEnabled(bool enable)
        {
            throw new NotImplementedException();
        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            
        }
    }
}