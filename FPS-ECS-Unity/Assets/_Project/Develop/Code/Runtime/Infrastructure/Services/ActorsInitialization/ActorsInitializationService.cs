using FpsEcs.Runtime.Gameplay.Common;
using Leopotam.EcsLite;
using UnityEngine;

namespace FpsEcs.Runtime.Infrastructure.Services.ActorsInitialization
{
    public class ActorsInitializationService : IActorsInitializationService
    {
        public void Initialize(EcsWorld world)
        {
            var actors = Object.FindObjectsByType<Actor>(FindObjectsInactive.Include, FindObjectsSortMode.None);

            foreach (var actor in actors)
            {
                actor.Initialize(world);
            }
        }
    }
}