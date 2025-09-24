using Leopotam.EcsLite;

namespace FpsEcs.Runtime.Gameplay.Helpers
{
    public interface IAuthoring
    {
        void Convert(EcsWorld world, int entity);
    }
}