using Cysharp.Threading.Tasks;
using FpsEcs.Runtime.Configs.ScriptableObjects;
using FpsEcs.Runtime.Infrastructure.Services.Pools;
using FpsEcs.Runtime.Utils.Enums;
using UnityEngine;

namespace FpsEcs.Runtime.Infrastructure.Factories
{
    public interface IGameFactory
    {
        UniTask Load();
        int CreatePlayer(Vector3 position, Quaternion rotation);
        int CreateEnemy(Vector3 position, Quaternion rotation);
        GameObject CreateEmptyObjectWithName(string name);
        ObjectPool CreatePool(Transform parentTransform, PoolConfig poolConfig);
        GameObject CreatePoolableObject(GameObject prefab, Transform parent, PoolId poolId, bool activeByDefault);
    }
}