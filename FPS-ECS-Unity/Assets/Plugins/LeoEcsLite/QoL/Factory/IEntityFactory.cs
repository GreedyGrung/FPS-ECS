using Leopotam.EcsLite;
using UnityEngine;

namespace LeoEcsLite.QoL.Factory
{
    public interface IEntityFactory
    {
        void Initialize(EcsWorld world);
        IEntityBuilder Create();
        int Convert(GameObject gameObject);
    }
}