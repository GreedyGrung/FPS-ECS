using FpsEcs.Runtime.Gameplay.Common.Modules;
using FpsEcs.Runtime.Gameplay.UI.Systems;

namespace FpsEcs.Runtime.Gameplay.UI
{
    public class UIModule : IEcsModule
    {
        public void Register(EcsModuleBuilder builder)
        {
            builder.AddRange(
                new UIInitializationSystem(),
                new HudRedrawSystem(),
                new UIViewsOpenCloseSystem());
        }
    }
}