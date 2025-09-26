using FpsEcs.Runtime.Configs.Implementations;
using UnityEngine;

namespace FpsEcs.Runtime.Configs.ScriptableObjects
{
    [CreateAssetMenu(fileName = "WeaponConfig", menuName = "Configs/Weapon")]
    public class WeaponConfigSO : ScriptableObject
    {
        public WeaponConfig Config;
    }
}