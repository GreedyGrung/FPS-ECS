using FpsEcs.Runtime.Gameplay.Input.Components;
using FpsEcs.Runtime.Gameplay.ProgressionFeature.Components;

namespace FpsEcs.Runtime.Gameplay.Common.Modules
{
    public class CleanupModule : IEcsModule
    {
        public void Register(EcsModuleBuilder builder)
        {
            builder
                .DelHere<PauseEvent>()
                .DelHere<ApplyUpgradesEvent>()
                .DelHere<SaveProgressEvent>();
        }
    }
}