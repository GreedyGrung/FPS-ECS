using FpsEcs.Runtime.Gameplay.Player.Systems;
using LeoEcsLite.QoL.Modules;

namespace FpsEcs.Runtime.Gameplay.Player
{
    public class PlayerModule : IEcsModule
    {
        public void Register(EcsModuleBuilder builder)
        {
            builder.AddRange(
                new SpawnPlayerSystem(),
                new CameraInitializationSystem(),
                new MovePlayerSystem(),
                new PlayerShootSystem(),
                new CameraLookSystem());
        }
    }
}