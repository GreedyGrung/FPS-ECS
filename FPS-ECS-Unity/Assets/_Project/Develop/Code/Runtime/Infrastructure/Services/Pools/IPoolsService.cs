using FpsEcs.Runtime.Utils.Enums;

namespace FpsEcs.Runtime.Infrastructure.Services.Pools
{
    public interface IPoolsService
    {
        void RegisterPool(PoolId type);
        void Dispose();
    }
}