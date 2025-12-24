using FpsEcs.Runtime.Gameplay.Weapons.Components;
using LeoEcsLite.QoL.Authoring;
using LeoEcsLite.QoL.Utils;
using Leopotam.EcsLite;
using UnityEngine;

namespace FpsEcs.Runtime.Gameplay.Weapons.Authorings
{
    public class WeaponSwayAuthoring : MonoBehaviour, IAuthoring
    {
        public void Convert(EcsWorld world, int entity)
        {
            entity.Add<WeaponSway>();
        }
    }
}