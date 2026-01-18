using FpsEcs.Runtime.Gameplay.UI.Components;
using LeoEcsLite.QoL.Authoring;
using LeoEcsLite.QoL.Utils;
using Leopotam.EcsLite;
using TMPro;
using UnityEngine;

namespace FpsEcs.Runtime.Gameplay.UI.Authorings
{
    public class EnemyCounterViewAuthoring : AuthoringBase
    {
        [SerializeField] private TextMeshProUGUI _enemyCounterText;
        
        public override void Convert(EcsWorld world, int entity)
        {
            ref var enemyCounter = ref entity.Add<EnemyCounterViewComponent>();
            enemyCounter.Value = _enemyCounterText;
        }
    }
}
