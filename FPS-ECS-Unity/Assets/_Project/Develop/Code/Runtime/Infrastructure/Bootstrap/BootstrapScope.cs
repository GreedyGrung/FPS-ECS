using FpsEcs.Runtime.Infrastructure.Services.AssetManagement;
using FpsEcs.Runtime.Infrastructure.Services.Configs;
using FpsEcs.Runtime.Infrastructure.Services.Input;
using FpsEcs.Runtime.Infrastructure.Services.Input.ScriptableObjects;
using FpsEcs.Runtime.Infrastructure.Services.Localization;
using FpsEcs.Runtime.Infrastructure.Services.SaveLoad;
using FpsEcs.Runtime.Infrastructure.Services.SceneLoading;
using FpsEcs.Runtime.Infrastructure.Services.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace FpsEcs.Runtime.Infrastructure.Bootstrap
{
    public class BootstrapScope : LifetimeScope
    {
        [SerializeField] private InputMapsProvider _inputMapsProvider;
        
        protected override void Awake()
        {
            DontDestroyOnLoad(this);
            base.Awake();
        }

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<ISceneLoader, SceneLoader>(Lifetime.Singleton);
            builder.Register<IAssetProvider, AssetProvider>(Lifetime.Singleton);
            builder.Register<IConfigsProvider, ConfigsProvider>(Lifetime.Singleton);
            builder.Register<IUIService, UIService>(Lifetime.Singleton);
            builder.Register<ILocalizationService, MockLocalizationService>(Lifetime.Singleton);
            builder.Register<ISaveLoadService, SaveLoadService>(Lifetime.Singleton);
            builder.RegisterInstance(_inputMapsProvider);
            builder.Register<IInputService, InputService>(Lifetime.Singleton);
            
            builder.RegisterEntryPoint<BootstrapFlow>();
        }
    }
}
