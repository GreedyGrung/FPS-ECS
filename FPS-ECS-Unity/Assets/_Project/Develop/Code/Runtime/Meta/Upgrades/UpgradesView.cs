using FpsEcs.Runtime.Infrastructure.Services.Input;
using FpsEcs.Runtime.Infrastructure.Services.Pause;
using FpsEcs.Runtime.UI;
using VContainer;

namespace FpsEcs.Runtime.Meta.Upgrades
{
    public class UpgradesView : UIPanelBase
    {
        private IPauseService _pauseService;
        private IInputService _inputService;

        [Inject]
        private void Construct(IPauseService pauseService, IInputService inputService)
        {
            _inputService = inputService;
            _pauseService = pauseService;
        }

        protected override void Close()
        {
            base.Close();
            
            _pauseService.TogglePause();
            _inputService.SwitchInputMaps();
        }
    }
}