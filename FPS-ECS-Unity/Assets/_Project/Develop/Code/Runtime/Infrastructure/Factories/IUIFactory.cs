using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using FpsEcs.Runtime.UI;
using FpsEcs.Runtime.Utils.Enums;
using UnityEngine;

namespace FpsEcs.Runtime.Infrastructure.Factories
{
    public interface IUIFactory
    {
        UniTask CreateUIRootAsync();
        UniTask<Dictionary<UIPanelId, UIPanelBase>> CreateUIPanelsAsync();
        UniTask Load();
        GameObject CreateHud();
    }
}