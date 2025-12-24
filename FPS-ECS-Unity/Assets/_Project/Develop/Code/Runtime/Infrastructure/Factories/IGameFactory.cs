using Cysharp.Threading.Tasks;
using FpsEcs.Runtime.Infrastructure.Services.Pools;
using UnityEngine;

namespace FpsEcs.Runtime.Infrastructure.Factories
{
    public interface IGameFactory
    {
        UniTask Load();
        int CreatePlayer(Vector3 position, Quaternion rotation);
        int CreateEnemy(Vector3 position, Quaternion rotation);
        T CreatePoolableObject<T>(Transform parent, bool activeByDefault) where T : IPoolableObject;
    }
}