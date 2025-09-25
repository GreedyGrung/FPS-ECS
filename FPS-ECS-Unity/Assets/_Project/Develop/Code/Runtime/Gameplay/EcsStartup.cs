using FpsEcs.Runtime.Gameplay.Enemies.Systems;
using FpsEcs.Runtime.Gameplay.HealthFeature.Systems;
using FpsEcs.Runtime.Gameplay.Input.Systems;
using FpsEcs.Runtime.Gameplay.MovementLogic.Systems;
using FpsEcs.Runtime.Gameplay.Player.Systems;
using FpsEcs.Runtime.Gameplay.Weapons.Systems;
using FpsEcs.Runtime.Infrastructure.Factories;
using FpsEcs.Runtime.Infrastructure.Services.ActorsInitialization;
using FpsEcs.Runtime.Infrastructure.Services.Configs;
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
        private IConfigsProvider _configsProvider;
        private IActorsInitializationService _actorsInitializationService;

        [Inject]
        private void Construct(
            IInputService inputService,
            IGameFactory gameFactory,
            IConfigsProvider configsProvider,
            IActorsInitializationService actorsInitializationService)
        {
            _actorsInitializationService = actorsInitializationService;
            _configsProvider = configsProvider;
            _gameFactory = gameFactory;
            _inputService = inputService;
        }
        
        public void Initialize() 
        {
            _world = new EcsWorld();
            _actorsInitializationService.Initialize(_world);
            _systems = new EcsSystems(_world);
            _systems
                .Add(new InputInitializationSystem())
                .Add(new CameraInitializationSystem())
                .Add(new PlayerHealthInitializationSystem())
                .Add(new WeaponInitializationSystem())
                .Add(new EnemyHealthInitializationSystem())
                .Add(new EnemiesSpawnSystem())
                .Add(new EnemiesInitializationSystem())
                .Add(new EnemiesMoveSystem())
                .Add(new MovementInitializationSystem())
                .Add(new InputReadSystem())
                .Add(new SpawnPlayerSystem())
                .Add(new CameraLookSystem())
                .Add(new MovePlayerSystem())
                .Add(new PlayerShootSystem())
                .Add(new ApplyDamageSystem())
                .Add(new DeathSystem())
#if UNITY_EDITOR
                .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
                .Add (new Leopotam.EcsLite.UnityEditor.EcsSystemsDebugSystem())
#endif
                .Inject(_inputService)
                .Inject(_gameFactory)
                .Inject(_configsProvider)
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