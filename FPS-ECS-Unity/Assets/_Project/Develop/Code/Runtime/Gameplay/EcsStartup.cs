using FpsEcs.Runtime.Gameplay.Common.Modules;
using FpsEcs.Runtime.Gameplay.Enemies;
using FpsEcs.Runtime.Gameplay.HealthFeature;
using FpsEcs.Runtime.Gameplay.Input;
using FpsEcs.Runtime.Gameplay.MovementLogic;
using FpsEcs.Runtime.Gameplay.Player;
using FpsEcs.Runtime.Gameplay.ProgressionFeature;
using FpsEcs.Runtime.Gameplay.UI;
using FpsEcs.Runtime.Gameplay.Weapons;
using FpsEcs.Runtime.Infrastructure.Factories;
using FpsEcs.Runtime.Infrastructure.Services.ActorsInitialization;
using FpsEcs.Runtime.Infrastructure.Services.Configs;
using FpsEcs.Runtime.Infrastructure.Services.Input;
using FpsEcs.Runtime.Infrastructure.Services.Pause;
using FpsEcs.Runtime.Infrastructure.Services.Pools;
using FpsEcs.Runtime.Infrastructure.Services.SaveLoad;
using FpsEcs.Runtime.Infrastructure.Services.UI;
using FpsEcs.Runtime.Infrastructure.Services.Upgrades;
using LeoEcsLite.QoL.Factory;
using LeoEcsLite.QoL.Utils;
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
        private IUIService _uiService;
        private IPauseService _pauseService;
        private IUIFactory _uiFactory;
        private IUpgradesService _upgradesService;
        private ISaveLoadService _saveLoadService;
        private IEntityFactory _entityFactory;
        private IPoolsService _poolsService;

        [Inject]
        private void Construct(
            IInputService inputService,
            IGameFactory gameFactory,
            IConfigsProvider configsProvider,
            IActorsInitializationService actorsInitializationService,
            IUIService uiService,
            IPauseService pauseService,
            IUIFactory uiFactory,
            IUpgradesService upgradesService,
            ISaveLoadService saveLoadService,
            IEntityFactory entityFactory,
            IPoolsService poolsService)
        {
            _poolsService = poolsService;
            _uiFactory = uiFactory;
            _inputService = inputService;
            _gameFactory = gameFactory;
            _configsProvider = configsProvider;
            _actorsInitializationService = actorsInitializationService;
            _uiService = uiService;
            _pauseService = pauseService;
            _upgradesService = upgradesService;
            _saveLoadService = saveLoadService;
            _entityFactory = entityFactory;
        }
        
        public void Initialize() 
        {
            _world = new EcsWorld();
            EcsUtils.Initialize(_world);
            _actorsInitializationService.Initialize(_world);
            _upgradesService.Initialize(_world);
            _entityFactory.Initialize(_world);
            _systems = new EcsSystems(_world);
            _systems
                .AddModule(new WeaponsModule())
                .AddModule(new InputModule())
                .AddModule(new HealthModule())
                .AddModule(new MovementModule())
                .AddModule(new PlayerModule())
                .AddModule(new EnemiesModule())
                .AddModule(new UIModule())
                .AddModule(new ProgressionModule())
                .AddModule(new DebugModule())
                .AddModule(new CleanupModule())
                .Inject(_inputService)
                .Inject(_gameFactory)
                .Inject(_configsProvider)
                .Inject(_uiService)
                .Inject(_pauseService)
                .Inject(_uiFactory)
                .Inject(_saveLoadService)
                .Inject(_entityFactory)
                .Inject(_poolsService)
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
            
            EcsUtils.Dispose();
        }
    }
}