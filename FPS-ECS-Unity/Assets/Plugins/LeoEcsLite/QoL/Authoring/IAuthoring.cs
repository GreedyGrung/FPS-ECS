using Leopotam.EcsLite;

namespace LeoEcsLite.QoL.Authoring
{
    public interface IAuthoring
    {
        void Convert(EcsWorld world, int entity);
    }
}