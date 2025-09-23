using FpsEcs.Runtime.Infrastructure.Services.Input.ScriptableObjects;
using UnityEngine.InputSystem;

namespace FpsEcs.Runtime.Infrastructure.Services.Input
{
    public class StandaloneInputService : IInputService
    {
        private readonly GameplayInputMap _gameplayInputMap;
        private readonly PauseInputMap _pauseInputMap;

        public StandaloneInputService(InputMapsProvider provider)
        {
            _gameplayInputMap = provider.GameplayInputMap;
            _pauseInputMap = provider.PauseInputMap;

            SetGameplayInputEnabled(true);
        }
        
        public InputAction MoveAction => _gameplayInputMap.MoveAction.action;
        public InputAction LookAction => _gameplayInputMap.LookAction.action;
        public InputAction AttackAction => _gameplayInputMap.AttackAction.action;
        public InputAction PauseAction => _gameplayInputMap.PauseAction;

        public void SetGameplayInputEnabled(bool enable)
        {
            _gameplayInputMap.EnableControls();
            _pauseInputMap.DisableControls();
        }

        public void SetPauseInputEnabled(bool enable)
        {
            _gameplayInputMap.DisableControls();
            _pauseInputMap.EnableControls();
        }

        public void Dispose()
        {
            
        }
    }
}