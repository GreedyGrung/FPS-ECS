using FpsEcs.Runtime.Gameplay;
using FpsEcs.Runtime.Infrastructure.Factories;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace FpsEcs.Runtime.Infrastructure.Gameplay
{
    public class GameplayScope : LifetimeScope
    {
        [SerializeField] private EcsStartup _startup;
        [SerializeField] private LevelDataProvider _levelDataProvider;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<IGameFactory, GameFactory>(Lifetime.Singleton);
            builder.RegisterInstance(_levelDataProvider);
            builder.RegisterEntryPoint<GameplayFlow>().WithParameter(_startup);
        }
    }
}