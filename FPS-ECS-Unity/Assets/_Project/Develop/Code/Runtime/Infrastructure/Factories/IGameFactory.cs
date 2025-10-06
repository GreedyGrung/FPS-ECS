using Cysharp.Threading.Tasks;
using UnityEngine;

namespace FpsEcs.Runtime.Infrastructure.Factories
{
    public interface IGameFactory
    {
        UniTask Load();
        int CreatePlayer(Vector3 position, Quaternion rotation);
        int CreateEnemy(Vector3 position, Quaternion rotation);
    }
}