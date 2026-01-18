using FpsEcs.Runtime.Gameplay.Enemies.Systems;
using LeoEcsLite.QoL.Modules;

namespace FpsEcs.Runtime.Gameplay.Enemies
{
    public class EnemiesModule : IEcsModule
    {
        public void Register(EcsModuleBuilder builder)
        {
            builder.AddRange(
                new EnemiesSpawnSystem(),
                new EnemiesInitializationSystem(),
                new EnemiesMoveSystem(),
                new EnemyCounterUpdateSystem());
        }
    }
}