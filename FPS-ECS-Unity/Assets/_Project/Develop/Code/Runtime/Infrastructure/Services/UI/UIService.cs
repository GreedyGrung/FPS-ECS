using System.Collections.Generic;
using FpsEcs.Runtime.UI;
using FpsEcs.Runtime.Utils.Enums;

namespace FpsEcs.Runtime.Infrastructure.Services.UI
{
    public class UIService : IUIService
    {
        private Dictionary<UIPanelId, UIPanelBase> _panels;
        
        public void Initialize(Dictionary<UIPanelId, UIPanelBase> panels) 
            => _panels = new(panels);

        public void Open(UIPanelId id) => _panels[id].gameObject.SetActive(true);
        
        public void Close(UIPanelId id) => _panels[id].gameObject.SetActive(false);
        
        public void CloseAll()
        {
            foreach (var panel in _panels)
            {
                panel.Value.gameObject.SetActive(false);
            }
        }
    }
}