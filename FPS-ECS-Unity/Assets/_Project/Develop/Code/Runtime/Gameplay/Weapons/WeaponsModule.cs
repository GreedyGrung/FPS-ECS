using FpsEcs.Runtime.Gameplay.Weapons.Systems;
using LeoEcsLite.QoL.Modules;

namespace FpsEcs.Runtime.Gameplay.Weapons
{
    public class WeaponsModule : IEcsModule
    {
        public void Register(EcsModuleBuilder builder)
        {
            builder.AddRange(
                new WeaponInitializationSystem(),
                new WeaponSwaySystem());
        }
    }
}