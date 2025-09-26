using System;

namespace FpsEcs.Runtime.Infrastructure.Services.SaveLoad.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public float Health;
        public float Speed;
        public float Damage;
        
        public int HealthUpgradeLevel;
        public int DamageUpgradeLevel;
        public int SpeedUpgradeLevel;

        public int AvailableUpgradePoints;
    }
}