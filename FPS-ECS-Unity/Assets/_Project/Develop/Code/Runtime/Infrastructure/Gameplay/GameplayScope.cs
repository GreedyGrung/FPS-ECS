using FpsEcs.Runtime.Gameplay;
using FpsEcs.Runtime.Infrastructure.Factories;
using FpsEcs.Runtime.Infrastructure.Services.ActorsInitialization;
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
            builder.Register<IActorsInitializationService, ActorsInitializationService>(Lifetime.Singleton);
            builder.RegisterEntryPoint<GameplayFlow>().WithParameter(_startup);
        }
    }
}