using FpsEcs.Runtime.Gameplay.Input.Systems;
using FpsEcs.Runtime.Infrastructure.Services.Input;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using VContainer;

namespace FpsEcs.Runtime.Gameplay
{
    public class EcsStartup : MonoBehaviour
    {
        private EcsWorld _world;
        private IEcsSystems _systems;
        private IInputService _inputService;

        [Inject]
        private void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }
        
        private void Start() 
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
            _systems
                .Add(new InputInitializationSystem())
                .Add(new InputReadSystem())
#if UNITY_EDITOR
                .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
                .Add (new Leopotam.EcsLite.UnityEditor.EcsSystemsDebugSystem())
#endif
                .Inject(_inputService)
                .Init();
        }
    
        private void Update() 
        {
            _systems?.Run();
        }

        private void OnDestroy() 
        {
            if (_systems != null) 
            {
                _systems.Destroy();
                _systems = null;
            }
            
            if (_world != null)
            {
                _world.Destroy();
                _world = null;
            }
        }
    }
}