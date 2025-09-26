using FpsEcs.Runtime.Infrastructure.Services.UI;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FpsEcs.Runtime.Gameplay.UI.Systems
{
    public class UIInitializationSystem : IEcsInitSystem
    {
        private readonly EcsCustomInject<IUIService> _uiService;
        
        public void Init(IEcsSystems systems)
        {
            _uiService.Value.CloseAll();
        }
    }
}