namespace FpsEcs.Runtime.Infrastructure.Services.Pools
{
    public interface IPoolableObject
    {
        void OnSpawned();
        void OnDespawned();
    }
}