using FpsEcs.Runtime.Gameplay.Enemies.Systems;
using FpsEcs.Runtime.Gameplay.HealthFeature.Systems;
using FpsEcs.Runtime.Gameplay.Input.Components;
using FpsEcs.Runtime.Gameplay.Input.Systems;
using FpsEcs.Runtime.Gameplay.MovementLogic.Systems;
using FpsEcs.Runtime.Gameplay.Player.Systems;
using FpsEcs.Runtime.Gameplay.ProgressionFeature.Systems;
using FpsEcs.Runtime.Gameplay.UI.Systems;
using FpsEcs.Runtime.Gameplay.Weapons.Systems;
using FpsEcs.Runtime.Infrastructure.Factories;
using FpsEcs.Runtime.Infrastructure.Services.ActorsInitialization;
using FpsEcs.Runtime.Infrastructure.Services.Configs;
using FpsEcs.Runtime.Infrastructure.Services.Input;
using FpsEcs.Runtime.Infrastructure.Services.Pause;
using FpsEcs.Runtime.Infrastructure.Services.UI;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.ExtendedSystems;
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
        private IUIService _uiService;
        private IPauseService _pauseService;

        [Inject]
        private void Construct(
            IInputService inputService,
            IGameFactory gameFactory,
            IConfigsProvider configsProvider,
            IActorsInitializationService actorsInitializationService,
            IUIService uiService,
            IPauseService pauseService)
        {
            _inputService = inputService;
            _gameFactory = gameFactory;
            _configsProvider = configsProvider;
            _actorsInitializationService = actorsInitializationService;
            _uiService = uiService;
            _pauseService = pauseService;
        }
        
        public void Initialize() 
        {
            _world = new EcsWorld();
            _actorsInitializationService.Initialize(_world);
            _systems = new EcsSystems(_world);
            _systems
                .Add(new InputInitializationSystem())
                .Add(new CameraInitializationSystem())
                .Add(new ProgressionInitializationSystem())
                .Add(new PlayerHealthInitializationSystem())
                .Add(new WeaponInitializationSystem())
                .Add(new EnemyHealthInitializationSystem())
                .Add(new EnemiesInitializationSystem())
                .Add(new MovementInitializationSystem())
                .Add(new UIInitializationSystem())
                .Add(new EnemiesSpawnSystem())
                .Add(new EnemiesMoveSystem())
                .Add(new InputReadSystem())
                .Add(new SpawnPlayerSystem())
                .Add(new CameraLookSystem())
                .Add(new MovePlayerSystem())
                .Add(new PlayerShootSystem())
                .Add(new ApplyDamageSystem())
                .Add(new EnemiesDeathObserverSystem())
                .Add(new DeathSystem())
                .Add(new WeaponSwaySystem())
                .Add(new UIViewsOpenCloseSystem())
#if UNITY_EDITOR
                .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
                .Add (new Leopotam.EcsLite.UnityEditor.EcsSystemsDebugSystem())
#endif
                .DelHere<PauseEvent>()
                .Inject(_inputService)
                .Inject(_gameFactory)
                .Inject(_configsProvider)
                .Inject(_uiService)
                .Inject(_pauseService)
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