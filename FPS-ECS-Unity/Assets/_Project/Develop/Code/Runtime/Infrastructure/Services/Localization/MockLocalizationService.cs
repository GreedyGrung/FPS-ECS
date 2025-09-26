using System.Collections.Generic;
using System.Xml;
using Cysharp.Threading.Tasks;
using FpsEcs.Runtime.Infrastructure.Services.AssetManagement;
using FpsEcs.Runtime.Utils;
using UnityEngine;

namespace FpsEcs.Runtime.Infrastructure.Services.Localization
{
    public class MockLocalizationService : ILocalizationService
    {
        private readonly IAssetProvider _assetProvider;
        private readonly Dictionary<string, List<string>> _localization = new();

        public MockLocalizationService(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }
        
        public async UniTask Load()
        {
            var textFile = await _assetProvider.Load<TextAsset>(Constants.Assets.LocalizationPath);
            
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(textFile.text);

            foreach (XmlNode key in xmlDocument["Keys"].ChildNodes)
            {
                string keyString = key.Attributes["name"].Value;

                var values = new List<string>();
                
                foreach (XmlNode translate in key["Translates"].ChildNodes)
                {
                    values.Add(translate.InnerText);
                }

                _localization[keyString] = values;
            }
        }
        
        public string GetTranslate(string key)
        {
            if (_localization.ContainsKey(key))
            {
                return _localization[key][0];
            }

            return key;
        }
    }
}