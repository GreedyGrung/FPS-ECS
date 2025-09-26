using Leopotam.EcsLite;

namespace FpsEcs.Runtime.Infrastructure.Services.Upgrades
{
    public interface IUpgradesService
    {
        void Initialize(EcsWorld world);
        void Apply(UpgradesPoints upgrades);
        int AvailablePoints { get; }
    }
}