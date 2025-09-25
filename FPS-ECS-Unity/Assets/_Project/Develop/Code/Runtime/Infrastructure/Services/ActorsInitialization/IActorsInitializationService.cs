using Leopotam.EcsLite;

namespace FpsEcs.Runtime.Infrastructure.Services.ActorsInitialization
{
    public interface IActorsInitializationService
    {
        void Initialize(EcsWorld world);
    }
}