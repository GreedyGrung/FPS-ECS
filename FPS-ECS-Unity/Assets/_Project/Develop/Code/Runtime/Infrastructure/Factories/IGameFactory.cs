using Cysharp.Threading.Tasks;
using UnityEngine;

namespace FpsEcs.Runtime.Infrastructure.Factories
{
    public interface IGameFactory
    {
        GameObject CreatePlayer(Vector3 position);
        UniTask Load();
    }
}