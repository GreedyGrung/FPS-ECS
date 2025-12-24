using FpsEcs.Runtime.Gameplay.Weapons.Components;
using LeoEcsLite.QoL.Authoring;
using LeoEcsLite.QoL.Utils;
using Leopotam.EcsLite;
using UnityEngine;

namespace FpsEcs.Runtime.Gameplay.Weapons.Authorings
{
    public class WeaponFireEffectAuthoring : MonoBehaviour, IAuthoring
    {
        [SerializeField] private ParticleSystem _fireEffect;
        
        public void Convert(EcsWorld world, int entity)
        {
            ref var effect = ref entity.Add<FireEffect>();
            effect.Value = _fireEffect;
        }
    }
}