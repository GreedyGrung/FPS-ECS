using System;
using UnityEngine.InputSystem;

namespace FpsEcs.Runtime.Infrastructure.Services.Input
{
    public interface IInputService : IDisposable
    {
        InputAction MoveAction { get; }
        InputAction LookAction { get; }
        InputAction AttackAction { get; }
        InputAction PauseAction { get; }
        
        void SetGameplayInputEnabled(bool enable);
        void SetPauseInputEnabled(bool enable);
    }
}