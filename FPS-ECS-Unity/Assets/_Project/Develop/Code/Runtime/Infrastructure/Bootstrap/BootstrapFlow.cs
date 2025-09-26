using FpsEcs.Runtime.Infrastructure.Services.Configs;
using FpsEcs.Runtime.Infrastructure.Services.Localization;
using FpsEcs.Runtime.Infrastructure.Services.SceneLoading;
using FpsEcs.Runtime.Utils;
using VContainer.Unity;

namespace FpsEcs.Runtime.Infrastructure.Bootstrap
{
    public class BootstrapFlow : IStartable
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IConfigsProvider _configsProvider;
        private readonly ILocalizationService _localizationService;

        public BootstrapFlow(
            ISceneLoader sceneLoader,
            IConfigsProvider configsProvider,
            ILocalizationService localizationService)
        {
            _sceneLoader = sceneLoader;
            _configsProvider = configsProvider;
            _localizationService = localizationService;
        }
        
        public async void Start()
        {
            await _configsProvider.Load();
            await _localizationService.Load();
            await _sceneLoader.Load(Constants.Scenes.Game);
        }
    }
}