using FpsEcs.Runtime.Gameplay.Player.Components;
using LeoEcsLite.QoL.Authoring;
using LeoEcsLite.QoL.Utils;
using Leopotam.EcsLite;

namespace FpsEcs.Runtime.Gameplay.Player.Authorings
{
    public class PlayerAuthoring : AuthoringBase
    {
        public override void Convert(EcsWorld world, int entity)
        {
            entity.Add<PlayerTag>();
        }
    }
}