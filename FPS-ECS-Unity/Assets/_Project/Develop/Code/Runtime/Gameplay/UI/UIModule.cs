using FpsEcs.Runtime.Gameplay.UI.Systems;
using LeoEcsLite.QoL.Modules;

namespace FpsEcs.Runtime.Gameplay.UI
{
    public class UIModule : IEcsModule
    {
        public void Register(EcsModuleBuilder builder)
        {
            builder.AddRange(
                new UIInitializationSystem(),
                new HudRedrawSystem(),
                new UIViewsOpenCloseSystem(),
                new EnemyCounterDisplaySystem());
        }
    }
}