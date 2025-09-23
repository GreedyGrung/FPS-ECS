using System;
using FpsEcs.Runtime.Configs.Interfaces;
using FpsEcs.Runtime.Utils;
using FpsEcs.Runtime.Utils.Enums;
using UnityEngine;

namespace FpsEcs.Runtime.Configs.Implementations
{
    [Serializable]
    public class WeaponConfig : IConfig
    {
        [field: SerializeField]
        public WeaponId Id { get; set; }
        [field: SerializeField]
        public float Damage { get; set; }
        [field: SerializeField]
        public float FireRate { get; set; }
    }
}