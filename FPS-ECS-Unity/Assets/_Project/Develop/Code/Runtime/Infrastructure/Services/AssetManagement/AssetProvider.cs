using Cysharp.Threading.Tasks;
using UnityEngine;

namespace FpsEcs.Runtime.Infrastructure.Services.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        public async UniTask<T> Load<T>(string address) where T : Object
        {
            await UniTask.Yield();
            
            return Resources.Load<T>(address);
        }
        
        public async UniTask<T[]> LoadAll<T>(string address) where T : Object
        {
            await UniTask.Yield();
            
            return Resources.LoadAll<T>(address);
        }
    }
}