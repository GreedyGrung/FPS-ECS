using FpsEcs.Runtime.Infrastructure.Services.AssetManagement;
using FpsEcs.Runtime.Infrastructure.Services.Configs;
using FpsEcs.Runtime.Infrastructure.Services.Input;
using FpsEcs.Runtime.Infrastructure.Services.SceneLoading;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace FpsEcs.Runtime.Infrastructure.Bootstrap
{
    public class BootstrapScope : LifetimeScope
    {
        protected override void Awake()
        {
            DontDestroyOnLoad(this);
            base.Awake();
        }

        protected override void Configure(IContainerBuilder builder)
        {
            RegisterInputService(builder);
            builder.Register<ISceneLoader, SceneLoader>(Lifetime.Singleton);
            builder.Register<IAssetProvider, AssetProvider>(Lifetime.Singleton);
            builder.Register<IConfigsProvider, ConfigsProvider>(Lifetime.Singleton);
            
            builder.RegisterEntryPoint<BootstrapFlow>();
        }

        private void RegisterInputService(IContainerBuilder builder)
        {
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                builder.Register<IInputService, MobileInputService>(Lifetime.Singleton);
            }
            else
            {
                builder.Register<IInputService, StandaloneInputService>(Lifetime.Singleton);
            }
        }
    }
}
