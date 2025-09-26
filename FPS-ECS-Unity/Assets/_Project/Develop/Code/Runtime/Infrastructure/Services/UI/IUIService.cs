using System.Collections.Generic;
using FpsEcs.Runtime.UI;
using FpsEcs.Runtime.Utils.Enums;

namespace FpsEcs.Runtime.Infrastructure.Services.UI
{
    public interface IUIService
    {
        void Initialize(Dictionary<UIPanelId, UIPanelBase> panels);
        void Open(UIPanelId id);
        void Close(UIPanelId id);
        void CloseAll();
    }
}