using FpsEcs.Runtime.Gameplay;
using FpsEcs.Runtime.Infrastructure.Factories;
using FpsEcs.Runtime.Infrastructure.Services.ActorsInitialization;
using FpsEcs.Runtime.Infrastructure.Services.Pause;
using FpsEcs.Runtime.Infrastructure.Services.Upgrades;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace FpsEcs.Runtime.Infrastructure.Gameplay
{
    public class GameplayScope : LifetimeScope
    {
        [SerializeField] private EcsStartup _startup;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<IGameFactory, GameFactory>(Lifetime.Singleton);
            builder.Register<IUIFactory, UIFactory>(Lifetime.Singleton);
            builder.Register<IActorsInitializationService, ActorsInitializationService>(Lifetime.Singleton);
            builder.Register<IPauseService, PauseService>(Lifetime.Singleton);
            builder.Register<IUpgradesService, UpgradesService>(Lifetime.Singleton);
            builder.Register<IEntityFactory, EntityFactory>(Lifetime.Singleton);
            builder.RegisterEntryPoint<GameplayFlow>().WithParameter(_startup);
        }
    }
}