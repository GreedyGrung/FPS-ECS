using FpsEcs.Runtime.Gameplay.Common.Modules;
using FpsEcs.Runtime.Gameplay.Input.Systems;

namespace FpsEcs.Runtime.Gameplay.Input
{
    public class InputModule : IEcsModule
    {
        public void Register(EcsModuleBuilder builder)
        {
            builder.AddRange(
                new InputInitializationSystem(), 
                new EnableMobileInputSystem(), 
                new InputReadSystem());
        }
    }
}