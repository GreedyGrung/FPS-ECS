using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FpsEcs.Runtime.Infrastructure.Services.SceneLoading
{
    public class SceneLoader : ISceneLoader
    {
        public async UniTask Load(string nextScene, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == nextScene)
            {
                onLoaded?.Invoke();
                await UniTask.Yield();
            }

            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);

            while (!waitNextScene.isDone)
            {
                await UniTask.Yield();
            }

            onLoaded?.Invoke();
        }
    }
}