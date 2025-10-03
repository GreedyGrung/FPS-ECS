using FpsEcs.Runtime.Gameplay.Common.Modules;
using FpsEcs.Runtime.Gameplay.Enemies.Systems;

namespace FpsEcs.Runtime.Gameplay.Enemies
{
    public class EnemiesModule : IEcsModule
    {
        public void Register(EcsModuleBuilder builder)
        {
            builder.AddRange(
                new EnemiesSpawnSystem(),
                new EnemiesInitializationSystem(),
                new EnemiesMoveSystem());
        }
    }
}