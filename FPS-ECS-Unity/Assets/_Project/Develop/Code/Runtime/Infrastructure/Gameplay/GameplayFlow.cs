using Cysharp.Threading.Tasks;
using FpsEcs.Runtime.Gameplay;
using FpsEcs.Runtime.Infrastructure.Factories;
using FpsEcs.Runtime.Infrastructure.Services.Pools;
using FpsEcs.Runtime.Infrastructure.Services.UI;
using FpsEcs.Runtime.Utils.Enums;
using VContainer.Unity;

namespace FpsEcs.Runtime.Infrastructure.Gameplay
{
    public class GameplayFlow : IStartable
    {
        private readonly IGameFactory _gameFactory;
        private readonly EcsStartup _ecsStartup;
        private readonly IUIFactory _uiFactory;
        private readonly IUIService _uiService;
        private readonly IPoolsService _poolsService;

        public GameplayFlow(IGameFactory gameFactory, EcsStartup ecsStartup, IUIFactory uiFactory, IUIService uiService, IPoolsService poolsService)
        {
            _gameFactory = gameFactory;
            _ecsStartup = ecsStartup;
            _uiFactory = uiFactory;
            _uiService = uiService;
            _poolsService = poolsService;
        }
        
        public async void Start()
        {
            await _gameFactory.Load();

            await InitializeGameUI();
            
            InitializeObjectPools();
            
            _ecsStartup.Initialize();
        }

        private async UniTask InitializeGameUI()
        {
            await _uiFactory.Load();
            await _uiFactory.CreateUIRootAsync();
            var views = await _uiFactory.CreateUIPanelsAsync();
            
            _uiService.Initialize(views);
        }

        private void InitializeObjectPools()
        {
            _poolsService.RegisterPool(PoolId.Enemy);
        }
    }
}