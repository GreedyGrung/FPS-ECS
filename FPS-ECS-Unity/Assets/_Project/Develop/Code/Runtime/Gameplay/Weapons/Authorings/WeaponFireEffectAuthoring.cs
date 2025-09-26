using FpsEcs.Runtime.Gameplay.Common;
using FpsEcs.Runtime.Gameplay.Weapons.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace FpsEcs.Runtime.Gameplay.Weapons.Authorings
{
    public class WeaponFireEffectAuthoring : MonoBehaviour, IAuthoring
    {
        [SerializeField] private ParticleSystem _fireEffect;
        
        public void Convert(EcsWorld world, int entity)
        {
            ref var effect = ref world.GetPool<FireEffect>().Add(entity);
            effect.Value = _fireEffect;
        }
    }
}