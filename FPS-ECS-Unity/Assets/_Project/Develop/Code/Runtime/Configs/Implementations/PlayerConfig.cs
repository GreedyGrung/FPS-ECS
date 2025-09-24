using System;
using FpsEcs.Runtime.Configs.Interfaces;
using UnityEngine;

namespace FpsEcs.Runtime.Configs.Implementations
{
    [Serializable]
    public class PlayerConfig : IEntityConfig
    {
        [field: SerializeField]
        public float Health { get; set; }
        [field: SerializeField]
        public float Speed { get; set; }
        [field: Header("Camera")]
        [field: SerializeField]
        public int MinPitch { get; set; }
        [field: SerializeField]
        public int MaxPitch { get; set; }
        [field: SerializeField]
        public float Sensitivity { get; set; }

        public override string ToString()
        {
            return string.Format($"Health: {Health}, Speed: {Speed}");
        }
    }
}