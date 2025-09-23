using System;

namespace FpsEcs.Runtime.Infrastructure.Services.PersistentProgress.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public float Health;
        public float Speed;
        public float Damage;

        public PlayerProgress(float health, float speed, float damage)
        {
            Health = health;
            Speed = speed;
            Damage = damage;
        }
    }
}