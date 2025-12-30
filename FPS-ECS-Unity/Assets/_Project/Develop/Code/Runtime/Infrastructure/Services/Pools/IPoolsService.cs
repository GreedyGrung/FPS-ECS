using FpsEcs.Runtime.Utils.Enums;
using UnityEngine;

namespace FpsEcs.Runtime.Infrastructure.Services.Pools
{
    public interface IPoolsService
    {
        void RegisterPool(PoolId id);
        void Dispose();
        GameObject GetFromPool(PoolId id);
        void ReturnToPool(PoolId id, GameObject prefab);
    }
}