using FpsEcs.Runtime.Infrastructure.Services.Configs;
using FpsEcs.Runtime.Infrastructure.Services.SceneLoading;
using FpsEcs.Runtime.Utils;
using FpsEcs.Runtime.Utils.Enums;
using UnityEngine;
using VContainer.Unity;

namespace FpsEcs.Runtime.Infrastructure.Bootstrap
{
    public class BootstrapFlow : IStartable
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IConfigsProvider _configsProvider;

        public BootstrapFlow(ISceneLoader sceneLoader, IConfigsProvider configsProvider)
        {
            _sceneLoader = sceneLoader;
            _configsProvider = configsProvider;
        }
        
        public async void Start()
        {
            Debug.Log("start!");

            await _configsProvider.Load();
            _sceneLoader.Load(Constants.Scenes.Game);

            var config = _configsProvider.GetWeaponConfig(WeaponId.AKM);
            Debug.Log($"config: {config}");
        }
    }
}