using FpsEcs.Runtime.Configs.Implementations;
using UnityEngine;

namespace FpsEcs.Runtime.Configs.ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/Player")]
    public class PlayerConfigSO : ScriptableObject
    {
        public PlayerConfig Config;
    }
}