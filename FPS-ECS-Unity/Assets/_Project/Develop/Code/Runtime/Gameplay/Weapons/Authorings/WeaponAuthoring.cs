using FpsEcs.Runtime.Gameplay.Common;
using FpsEcs.Runtime.Gameplay.Weapons.Components;
using FpsEcs.Runtime.Utils.Enums;
using Leopotam.EcsLite;
using UnityEngine;

namespace FpsEcs.Runtime.Gameplay.Weapons.Authorings
{
    public class WeaponAuthoring : MonoBehaviour, IAuthoring
    {
        [SerializeField] private WeaponId _id;
        
        public void Convert(EcsWorld world, int entity)
        {
            world.GetPool<WeaponInitializationNeededTag>().Add(entity);
            ref var weapon = ref world.GetPool<Weapon>().Add(entity);
            weapon.Id = _id;
        }
    }
}