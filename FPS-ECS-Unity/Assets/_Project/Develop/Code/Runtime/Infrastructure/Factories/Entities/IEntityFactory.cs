using Leopotam.EcsLite;
using UnityEngine;

namespace FpsEcs.Runtime.Infrastructure.Factories.Entities
{
    public interface IEntityFactory
    {
        void Initialize(EcsWorld world);
        IEntityBuilder Create();
        int Convert(GameObject gameObject);
    }
}
