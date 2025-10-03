using FpsEcs.Runtime.Gameplay.Common.Modules;
using FpsEcs.Runtime.Gameplay.HealthFeature.Systems;

namespace FpsEcs.Runtime.Gameplay.HealthFeature
{
    public class HealthModule : IEcsModule
    {
        public void Register(EcsModuleBuilder builder)
        {
            builder.AddRange(
                new PlayerHealthInitializationSystem(), 
                new EnemyHealthInitializationSystem(),
                new ApplyDamageSystem(),
                new DeathSystem());
        }
    }
}