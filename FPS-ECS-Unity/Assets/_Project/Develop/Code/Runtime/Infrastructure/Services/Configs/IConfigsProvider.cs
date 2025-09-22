using FpsEcs.Runtime.Configs;

namespace FpsEcs.Runtime.Infrastructure.Services.Configs
{
    public interface IConfigsProvider
    {
        void Load();
        T GetConfig<T>() where T : class, IConfig;
    }
}