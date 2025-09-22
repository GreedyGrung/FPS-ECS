using FpsEcs.Runtime.Infrastructure.Services.SceneLoading;
using FpsEcs.Runtime.Utils;
using UnityEngine;
using VContainer.Unity;

namespace FpsEcs.Runtime.Infrastructure.Bootstrap
{
    public class BootstrapFlow : IStartable
    {
        private readonly ISceneLoader _sceneLoader;

        public BootstrapFlow(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }
        
        public void Start()
        {
            Debug.Log("start!");

            _sceneLoader.Load(Constants.Scenes.Game);
        }
    }
}