namespace FpsEcs.Runtime.Gameplay.Common.Modules
{
    public interface IEcsModule
    {
        void Register(EcsModuleBuilder builder);
    }
}