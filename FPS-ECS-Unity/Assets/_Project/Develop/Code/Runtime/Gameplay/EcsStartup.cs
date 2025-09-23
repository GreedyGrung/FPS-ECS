using FpsEcs.Runtime.Gameplay.Input.Systems;
using FpsEcs.Runtime.Gameplay.Player;
using FpsEcs.Runtime.Gameplay.Player.Systems;
using FpsEcs.Runtime.Infrastructure.Factories;
using FpsEcs.Runtime.Infrastructure.Services.Input;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using VContainer;

namespace FpsEcs.Runtime.Gameplay
{
    public class EcsStartup : MonoBehaviour
    {
        private EcsWorld _world;
        private IEcsSystems _systems;
        private IInputService _inputService;
        private IGameFactory _gameFactory;
        private LevelDataProvider _levelDataProvider;

        [Inject]
        private void Construct(IInputService inputService, IGameFactory gameFactory, LevelDataProvider levelDataProvider)
        {
            _levelDataProvider = levelDataProvider;
            _gameFactory = gameFactory;
            _inputService = inputService;
        }
        
        public void Initialize() 
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
            _systems
                .Add(new InputInitializationSystem())
                .Add(new InputReadSystem())
                .Add(new SpawnPlayerSystem())
                .Add(new CameraLookSystem())
                .Add(new MovePlayerSystem())
#if UNITY_EDITOR
                .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
                .Add (new Leopotam.EcsLite.UnityEditor.EcsSystemsDebugSystem())
#endif
                .Inject(_inputService)
                .Inject(_gameFactory)
                .Inject(_levelDataProvider)
                .Init();
        }
    
        private void Update() 
        {
            _systems?.Run();
        }

        private void OnDestroy() 
        {
            if (_systems != null) 
            {
                _systems.Destroy();
                _systems = null;
            }
            
            if (_world != null)
            {
                _world.Destroy();
                _world = null;
            }
        }
    }
}