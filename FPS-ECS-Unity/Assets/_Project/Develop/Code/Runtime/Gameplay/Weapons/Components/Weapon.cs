using FpsEcs.Runtime.Utils.Enums;

namespace FpsEcs.Runtime.Gameplay.Weapons.Components
{
    public struct Weapon
    {
        public WeaponId Id;
        public float FireRate;
        public float Damage;
        public float MaxDistance;
        public float SpreadDegrees; 
        public int LayerMask;
    }
}