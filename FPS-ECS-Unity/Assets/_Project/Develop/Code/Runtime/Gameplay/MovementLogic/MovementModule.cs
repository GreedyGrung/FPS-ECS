using FpsEcs.Runtime.Gameplay.MovementLogic.Systems;
using LeoEcsLite.QoL.Modules;

namespace FpsEcs.Runtime.Gameplay.MovementLogic
{
    public class MovementModule : IEcsModule
    {
        public void Register(EcsModuleBuilder builder)
        {
            builder.Add(new MovementInitializationSystem());
        }
    }
}