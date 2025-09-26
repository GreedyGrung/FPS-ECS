using Cysharp.Threading.Tasks;
using FpsEcs.Runtime.Gameplay;
using FpsEcs.Runtime.Infrastructure.Factories;
using FpsEcs.Runtime.Infrastructure.Services.UI;
using VContainer.Unity;

namespace FpsEcs.Runtime.Infrastructure.Gameplay
{
    public class GameplayFlow : IStartable
    {
        private readonly IGameFactory _gameFactory;
        private readonly EcsStartup _ecsStartup;
        private readonly IUIFactory _uiFactory;
        private readonly IUIService _uiService;

        public GameplayFlow(IGameFactory gameFactory, EcsStartup ecsStartup, IUIFactory uiFactory, IUIService uiService)
        {
            _gameFactory = gameFactory;
            _ecsStartup = ecsStartup;
            _uiFactory = uiFactory;
            _uiService = uiService;
        }
        
        public async void Start()
        {
            await _gameFactory.Load();

            await InitializeGameUI();
            
            _ecsStartup.Initialize();
        }

        private async UniTask InitializeGameUI()
        {
            await _uiFactory.Load();
            await _uiFactory.CreateUIRootAsync();
            var views = await _uiFactory.CreateUIPanelsAsync();
            
            _uiService.Initialize(views);
        }
    }
}