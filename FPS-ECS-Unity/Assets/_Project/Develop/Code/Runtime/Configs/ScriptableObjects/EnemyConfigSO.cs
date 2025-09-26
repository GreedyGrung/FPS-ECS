using FpsEcs.Runtime.Configs.Implementations;
using UnityEngine;

namespace FpsEcs.Runtime.Configs.ScriptableObjects
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "Configs/Enemy")]
    public class EnemyConfigSO : ScriptableObject
    {
        public EnemyConfig Config;
    }
}