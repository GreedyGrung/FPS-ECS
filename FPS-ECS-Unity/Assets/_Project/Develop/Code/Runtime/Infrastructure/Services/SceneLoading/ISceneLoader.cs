using System;
using Cysharp.Threading.Tasks;

namespace FpsEcs.Runtime.Infrastructure.Services.SceneLoading
{
    public interface ISceneLoader
    {
        UniTask Load(string nextScene, Action onLoaded = null);
    }
}