using Leopotam.EcsLite;

namespace FpsEcs.Runtime.Infrastructure.Services.Upgrades
{
    public interface IUpgradesService
    {
        void Initialize(EcsWorld world);
        void Apply(UpgradesData upgrades);
        int AvailablePoints { get; }
        UpgradesData GetUpgradesLevels();
    }
}