using UnityEngine;

namespace FpsEcs.Runtime.Infrastructure.Services.Input.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Input/Input Maps Provider", fileName = "InputMapsProvider")]
    public class InputMapsProvider : ScriptableObject
    {
        [SerializeField] private GameplayInputMap _gameplayInputMap;
        [SerializeField] private PauseInputMap _pauseInputMap;
        
        public GameplayInputMap GameplayInputMap => _gameplayInputMap;
        public PauseInputMap PauseInputMap => _pauseInputMap;
    }
}