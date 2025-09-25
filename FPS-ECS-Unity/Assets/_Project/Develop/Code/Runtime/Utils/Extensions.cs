using Leopotam.EcsLite;

namespace FpsEcs.Runtime.Utils
{
    public static class Extensions
    {
        public static int First(this EcsFilter filter) => filter.GetRawEntities()[0];
    }
}