using System.Collections.Generic;
using FpsEcs.Runtime.Configs;
using FpsEcs.Runtime.Infrastructure.Services.AssetManagement;

namespace FpsEcs.Runtime.Infrastructure.Services.Configs
{
    public class ConfigsProvider : IConfigsProvider
    {
        private readonly List<IConfig> _configs = new();
        private readonly IAssetProvider _assetProvider;

        public ConfigsProvider(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public void Load()
        {
            
        }
        
        public T GetConfig<T>() where T : class, IConfig 
            => _configs.Find(c => c.GetType() == typeof(T)) as T;
    }
}