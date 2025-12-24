using FpsEcs.Runtime.Gameplay.ProgressionFeature.Systems;
using LeoEcsLite.QoL.Modules;

namespace FpsEcs.Runtime.Gameplay.ProgressionFeature
{
    public class ProgressionModule : IEcsModule
    {
        public void Register(EcsModuleBuilder builder)
        {
            builder.AddRange(
                new ProgressionInitializationSystem(),
                new EnemiesDeathObserverSystem(),
                new ApplyUpgradesSystem(),
                new SavePlayerProgressSystem(),
                new LoadPlayerProgressSystem());
        }
    }
}