using FpsEcs.Runtime.Gameplay.Common.Components.UnityComponentsReferences;
using FpsEcs.Runtime.Gameplay.Player.Components;
using FpsEcs.Runtime.Infrastructure.Services.Configs;
using LeoEcsLite.QoL.Utils;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FpsEcs.Runtime.Gameplay.Player.Systems
{
    public class CameraInitializationSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorldInject _world;
        private readonly EcsCustomInject<ConfigsProvider> _configsProvider;
        
        private EcsFilter _cameraFilter;
        
        private EcsWorld World => _world.Value;
        private ConfigsProvider ConfigsProvider => _configsProvider.Value;
        
        public void Init(IEcsSystems systems)
        {
            _cameraFilter = World.Inc<CameraInitializationNeededTag, CameraRef>().End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var camera in _cameraFilter)
            {
                camera.Add<CameraState>();
                
                ref var cameraState = ref camera.Add<CameraSettings>();
                var playerConfig = ConfigsProvider.GetPlayerConfig();
                
                cameraState.MinPitch = playerConfig.MinPitch;
                cameraState.MaxPitch = playerConfig.MaxPitch;
                cameraState.Sensitivity = playerConfig.Sensitivity;
                
                camera.Del<CameraInitializationNeededTag>();
            }
        }
    }
}