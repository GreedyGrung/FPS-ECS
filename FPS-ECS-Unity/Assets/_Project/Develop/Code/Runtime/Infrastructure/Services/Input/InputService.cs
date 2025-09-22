using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FpsEcs.Runtime.Infrastructure.Services.Input
{
    public class StandaloneInputService : IInputService
    {
        public event Action OnPauseInput;
        
        private readonly PlayerControls _playerControls;

        public StandaloneInputService()
        {
            _playerControls = new PlayerControls();
            _playerControls.Enable();
            
            Subscribe();
        }
        
        public int InputX { get; private set; }
        public int InputY { get; private set; }
        public bool AttackInput { get; private set; }
        
        public void Dispose() => Unsubscribe();

        private void Subscribe()
        {
            _playerControls.Gameplay.Move.performed += Move;
            _playerControls.Gameplay.Move.canceled += Move;

            _playerControls.Gameplay.Attack.started += StartAttack;
            _playerControls.Gameplay.Attack.canceled += CancelAttack;

            _playerControls.Gameplay.Pause.performed += Pause;
        }

        private void Unsubscribe()
        {
            _playerControls.Gameplay.Move.performed -= Move;
            _playerControls.Gameplay.Move.canceled -= Move;

            _playerControls.Gameplay.Attack.started -= StartAttack;
            _playerControls.Gameplay.Attack.canceled -= CancelAttack;

            _playerControls.Gameplay.Pause.performed -= Pause;
        }

        private void Move(InputAction.CallbackContext context)
        {
            var rawInput = context.ReadValue<Vector2>();
            
            InputX = Mathf.RoundToInt(rawInput.x);
            InputY = Mathf.RoundToInt(rawInput.y);
        }

        private void StartAttack(InputAction.CallbackContext context) => AttackInput = true;

        private void CancelAttack(InputAction.CallbackContext context) => AttackInput = false;

        private void Pause(InputAction.CallbackContext context) => OnPauseInput?.Invoke();
    }
}