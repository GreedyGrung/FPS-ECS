using Leopotam.EcsLite;
using UnityEngine;

namespace FpsEcs.Runtime.Gameplay.Common
{
    public static class EntityFactory
    {
        public static int Create(EcsWorld world)
        {
            return world.NewEntity();
        }

        public static int CreateFrom(GameObject gameObject, EcsWorld world)
        {
            var actor = gameObject.GetComponent<Actor>();
            actor.Initialize(world);
            
            return actor.GetEntity();
        }
    }
}