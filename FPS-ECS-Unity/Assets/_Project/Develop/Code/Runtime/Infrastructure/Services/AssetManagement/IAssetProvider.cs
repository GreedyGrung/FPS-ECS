using Cysharp.Threading.Tasks;
using UnityEngine;

namespace FpsEcs.Runtime.Infrastructure.Services.AssetManagement
{
    public interface IAssetProvider
    {
        UniTask<T> Load<T>(string address) where T : Object;
    }
}