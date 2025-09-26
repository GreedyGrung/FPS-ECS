using System;
using FpsEcs.Runtime.Gameplay.Common;
using FpsEcs.Runtime.Infrastructure.Factories;
using FpsEcs.Runtime.Infrastructure.Services.UI;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FpsEcs.Runtime.Gameplay.UI.Systems
{
    public class UIInitializationSystem : IEcsInitSystem
    {
        private readonly EcsCustomInject<IUIService> _uiService;
        private readonly EcsCustomInject<IUIFactory> _uiFactory;
        private readonly EcsCustomInject<IEntityFactory> _entityFactory;
        
        public void Init(IEcsSystems systems)
        {
            _uiService.Value.CloseAll();

            var hud = _uiFactory.Value.CreateHud();
            _entityFactory.Value.CreateFrom(hud);
        }
    }
}