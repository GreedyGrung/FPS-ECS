using FpsEcs.Runtime.Gameplay.Common;
using FpsEcs.Runtime.Gameplay.Weapons.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace FpsEcs.Runtime.Gameplay.Weapons.Authorings
{
    public class WeaponSwayAuthoring : MonoBehaviour, IAuthoring
    {
        public void Convert(EcsWorld world, int entity)
        {
            world.GetPool<WeaponSway>().Add(entity);
        }
    }
}