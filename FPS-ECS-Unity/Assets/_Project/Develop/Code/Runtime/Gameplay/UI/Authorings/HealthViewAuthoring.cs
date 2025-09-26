using FpsEcs.Runtime.Gameplay.Common;
using FpsEcs.Runtime.Gameplay.UI.Components;
using Leopotam.EcsLite;
using TMPro;
using UnityEngine;

namespace FpsEcs.Runtime.Gameplay.UI.Authorings
{
    public class HealthViewAuthoring : MonoBehaviour, IAuthoring
    {
        [SerializeField] private TextMeshProUGUI _healthText;
        
        public void Convert(EcsWorld world, int entity)
        {
            ref var health = ref world.GetPool<HealthViewComponent>().Add(entity);
            health.Value = _healthText;
        }
    }
}