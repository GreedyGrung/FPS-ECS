using FpsEcs.Runtime.Gameplay.Common.Modules;
using FpsEcs.Runtime.Gameplay.MovementLogic.Systems;

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