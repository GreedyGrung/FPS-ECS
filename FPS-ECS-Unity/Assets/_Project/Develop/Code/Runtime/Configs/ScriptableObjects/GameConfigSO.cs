using FpsEcs.Runtime.Configs.Implementations;
using UnityEngine;

namespace FpsEcs.Runtime.Configs.ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/Game")]
    public class GameConfigSO : ScriptableObject
    {
        public GameConfig Config;
    }
}