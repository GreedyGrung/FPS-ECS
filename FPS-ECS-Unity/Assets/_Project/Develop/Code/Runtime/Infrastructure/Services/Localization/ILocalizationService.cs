using Cysharp.Threading.Tasks;

namespace FpsEcs.Runtime.Infrastructure.Services.Localization
{
    public interface ILocalizationService
    {
        UniTask Load();
        string GetTranslate(string key);
    }
}