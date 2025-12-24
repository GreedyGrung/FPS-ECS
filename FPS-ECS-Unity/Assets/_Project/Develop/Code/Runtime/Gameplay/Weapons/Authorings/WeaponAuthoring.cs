using FpsEcs.Runtime.Gameplay.Weapons.Components;
using FpsEcs.Runtime.Utils.Enums;
using LeoEcsLite.QoL.Authoring;
using LeoEcsLite.QoL.Utils;
using Leopotam.EcsLite;
using UnityEngine;

namespace FpsEcs.Runtime.Gameplay.Weapons.Authorings
{
    public class WeaponAuthoring : MonoBehaviour, IAuthoring
    {
        [SerializeField] private WeaponId _id;
        
        public void Convert(EcsWorld world, int entity)
        {
            entity.Add<WeaponInitializationNeededTag>();
            ref var weapon = ref entity.Add<Weapon>();
            weapon.Id = _id;
        }
    }
}