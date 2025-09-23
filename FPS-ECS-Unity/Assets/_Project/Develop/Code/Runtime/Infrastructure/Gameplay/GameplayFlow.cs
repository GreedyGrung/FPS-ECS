using FpsEcs.Runtime.Gameplay;
using FpsEcs.Runtime.Infrastructure.Factories;
using UnityEngine;
using VContainer.Unity;

namespace FpsEcs.Runtime.Infrastructure.Gameplay
{
    public class GameplayFlow : IStartable
    {
        private readonly IGameFactory _gameFactory;
        private readonly EcsStartup _ecsStartup;

        public GameplayFlow(IGameFactory gameFactory, EcsStartup ecsStartup)
        {
            _gameFactory = gameFactory;
            _ecsStartup = ecsStartup;
        }
        
        public async void Start()
        {
            await _gameFactory.Load();
            Debug.LogError("flow!");
            
            _ecsStartup.Initialize();
        }
    }
}