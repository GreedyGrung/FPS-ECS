using FpsEcs.Runtime.Gameplay.Input.Components;
using FpsEcs.Runtime.Infrastructure.Services.Pause;
using FpsEcs.Runtime.Infrastructure.Services.UI;
using FpsEcs.Runtime.Utils.Enums;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FpsEcs.Runtime.Gameplay.UI.Systems
{
    public class UIViewsOpenCloseSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorldInject _world;
        private readonly EcsCustomInject<IPauseService> _pauseService;
        private readonly EcsCustomInject<IUIService> _uiService;
        
        private readonly EcsPoolInject<PauseEvent> _pausePool;
        
        private EcsFilter _pauseFilter;

        private EcsWorld World => _world.Value;
        private IPauseService PauseService => _pauseService.Value;
        private IUIService UIService => _uiService.Value;
        
        public void Init(IEcsSystems systems)
        {
            _pauseFilter = World
                .Filter<PauseEvent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var pauseEntity in _pauseFilter)
            {
                PauseService.TogglePause();
                
                if (PauseService.IsPaused)
                {
                    UIService.Open(UIPanelId.Upgrades);
                }
                else
                {
                    UIService.Close(UIPanelId.Upgrades);
                }
                
                _pausePool.Value.Del(pauseEntity);
            }
        }
    }
}