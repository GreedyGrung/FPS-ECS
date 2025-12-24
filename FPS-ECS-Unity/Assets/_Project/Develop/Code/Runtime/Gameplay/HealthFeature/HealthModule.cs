using FpsEcs.Runtime.Gameplay.HealthFeature.Systems;
using LeoEcsLite.QoL.Modules;

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