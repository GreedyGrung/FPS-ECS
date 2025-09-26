using FpsEcs.Runtime.Infrastructure.Services.SaveLoad.Data;

namespace FpsEcs.Runtime.Infrastructure.Services.SaveLoad
{
    public interface ISaveLoadService
    {
        void SaveProgress(PlayerProgress playerProgress);
        PlayerProgress LoadProgress();
        void DeleteProgress();
    }
}