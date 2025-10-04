using Leopotam.EcsLite;
using UnityEngine;

namespace FpsEcs.Runtime.Infrastructure.Factories
{
    public interface IEntityFactory
    {
        void Initialize(EcsWorld world);
        IEntityBuilder Create();
        int CreateFrom(GameObject gameObject);
    }

    public interface IEntityBuilder
    {
        IEntityBuilder With<T>() where T : struct;
        IEntityBuilder With<T>(in T component) where T : struct;
        int Build();
    }
}
