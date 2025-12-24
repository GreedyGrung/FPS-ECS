using Leopotam.EcsLite;
using UnityEngine;

namespace LeoEcsLite.QoL.Authoring
{
    [RequireComponent(typeof(ActorBase))]
    public abstract class AuthoringBase : MonoBehaviour, IAuthoring
    {
        public abstract void Convert(EcsWorld world, int entity);
    }
}