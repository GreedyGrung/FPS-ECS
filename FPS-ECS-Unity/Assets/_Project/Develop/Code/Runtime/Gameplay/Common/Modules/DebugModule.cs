using LeoEcsLite.QoL.Modules;

namespace FpsEcs.Runtime.Gameplay.Common.Modules
{
    public class DebugModule : IEcsModule
    {
        public void Register(EcsModuleBuilder builder)
        {
            #if UNITY_EDITOR
            builder.AddRange(
                new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem(), 
                new Leopotam.EcsLite.UnityEditor.EcsSystemsDebugSystem());
            #endif
        }
    }
}