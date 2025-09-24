using FpsEcs.Runtime.Gameplay.Common.Components.UnityComponentsReferences;
using FpsEcs.Runtime.Gameplay.Player.Components;
using FpsEcs.Runtime.Infrastructure.Services.Configs;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FpsEcs.Runtime.Gameplay.Player.Systems
{
    public class CameraInitializationSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorldInject _world;
        private readonly EcsCustomInject<ConfigsProvider> _configsProvider;
        
        private readonly EcsPoolInject<CameraInitializationNeededTag> _cameraInitPool = default;
        private readonly EcsPoolInject<CameraSettings> _cameraSettingsPool = default;
        private readonly EcsPoolInject<CameraState> _cameraStatePool = default;
        
        private EcsFilter _cameraFilter;
        
        private EcsWorld World => _world.Value;
        private ConfigsProvider ConfigsProvider => _configsProvider.Value;
        
        public void Init(IEcsSystems systems)
        {
            _cameraFilter = World
                .Filter<CameraInitializationNeededTag>()
                .Inc<CameraRef>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var camera in _cameraFilter)
            {
                _cameraStatePool.Value.Add(camera);
                
                ref var cameraState = ref _cameraSettingsPool.Value.Add(camera);
                cameraState.MinPitch = ConfigsProvider.GetPlayerConfig().MinPitch;
                cameraState.MaxPitch = ConfigsProvider.GetPlayerConfig().MaxPitch;
                cameraState.Sensitivity = ConfigsProvider.GetPlayerConfig().Sensitivity;
                
                _cameraInitPool.Value.Del(camera);
            }
        }
    }
}