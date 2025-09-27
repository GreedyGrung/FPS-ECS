using FpsEcs.Runtime.Infrastructure.Services.Localization;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace FpsEcs.Runtime.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LocalizedText : MonoBehaviour
    {
        private TextMeshProUGUI _text;
        private ILocalizationService _localizationService;

        private void Start()
        {
            _text = GetComponent<TextMeshProUGUI>();
            
            var scope = LifetimeScope.Find<LifetimeScope>(); // it is bad, but very convenient... 
            _localizationService = scope.Container.Resolve<ILocalizationService>();
            
            var key = _text.text;
            _text.text = _localizationService.GetTranslate(key);
        }
    }
}