using FpsEcs.Runtime.Gameplay.Input.Systems;
using LeoEcsLite.QoL.Modules;

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