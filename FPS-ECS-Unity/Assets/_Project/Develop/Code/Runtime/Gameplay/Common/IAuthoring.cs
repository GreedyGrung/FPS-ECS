using Leopotam.EcsLite;

namespace FpsEcs.Runtime.Gameplay.Common
{
    public interface IAuthoring
    {
        void Convert(EcsWorld world, int entity);
    }
}