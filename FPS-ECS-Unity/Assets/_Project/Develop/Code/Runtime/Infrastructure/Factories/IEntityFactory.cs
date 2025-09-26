using Leopotam.EcsLite;
using UnityEngine;

namespace FpsEcs.Runtime.Infrastructure.Factories
{
    public interface IEntityFactory
    {
        void Initialize(EcsWorld world);
        int Create();
        int CreateFrom(GameObject gameObject);
        EcsWorld World { get; }
    }
}