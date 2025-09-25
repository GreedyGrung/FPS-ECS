using System;
using UnityEngine.InputSystem;

namespace FpsEcs.Runtime.Infrastructure.Services.Input
{
    public interface IInputService : IDisposable
    {
        InputAction MoveAction { get; }
        InputAction LookAction { get; }
        InputAction AttackAction { get; }
        bool PauseActionThisFrame { get; }
        void SwitchInputMaps();
    }
}