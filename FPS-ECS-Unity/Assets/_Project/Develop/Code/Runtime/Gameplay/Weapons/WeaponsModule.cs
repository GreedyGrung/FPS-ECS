using FpsEcs.Runtime.Gameplay.Common.Modules;
using FpsEcs.Runtime.Gameplay.Weapons.Systems;

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