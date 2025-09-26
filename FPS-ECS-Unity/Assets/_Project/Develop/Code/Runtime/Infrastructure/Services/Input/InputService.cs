using FpsEcs.Runtime.Infrastructure.Services.Input.ScriptableObjects;
using UnityEngine.InputSystem;

namespace FpsEcs.Runtime.Infrastructure.Services.Input
{
    public class InputService : IInputService
    {
        private readonly GameplayInputMap _gameplayInputMap;
        private readonly PauseInputMap _pauseInputMap;

        public InputService(InputMapsProvider provider)
        {
            _gameplayInputMap = provider.GameplayInputMap;
            _pauseInputMap = provider.PauseInputMap;

            EnableGameplayInputMap();
        }
        
        public InputAction MoveAction => _gameplayInputMap.MoveAction.action;
        public InputAction LookAction => _gameplayInputMap.LookAction.action;
        public InputAction AttackAction => _gameplayInputMap.AttackAction.action;

        public bool PauseActionThisFrame => 
            _pauseInputMap.PauseAction.action.WasPressedThisFrame() ||
            _gameplayInputMap.PauseAction.action.WasPressedThisFrame();

        public void SwitchInputMaps()
        {
            if (_gameplayInputMap.IsEnabled)
            {
                EnablePauseInputMap();
            }
            else
            {
                EnableGameplayInputMap();
            }
        }
        
        private void EnableGameplayInputMap()
        {
            _gameplayInputMap.EnableControls();
            _pauseInputMap.DisableControls();
        }

        private void EnablePauseInputMap()
        {
            _gameplayInputMap.DisableControls();
            _pauseInputMap.EnableControls();
        }

        public void Dispose()
        {
            
        }
    }
}