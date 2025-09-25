using FpsEcs.Runtime.Gameplay.Enemies.Components;
using FpsEcs.Runtime.Gameplay.MovementLogic.Components;
using FpsEcs.Runtime.Gameplay.Player.Components;
using FpsEcs.Runtime.Infrastructure.Services.Configs;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FpsEcs.Runtime.Gameplay.MovementLogic.Systems
{
    public class MovementInitializationSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorldInject _world;
        private readonly EcsCustomInject<IConfigsProvider> _configsProvider;
        
        private EcsFilter _movementPlayerInitFilter;
        private EcsFilter _movementEnemiesInitFilter;
        
        private EcsWorld World => _world.Value;
        private IConfigsProvider ConfigsProvider => _configsProvider.Value;
        
        public void Init(IEcsSystems systems)
        {
            _movementPlayerInitFilter = World
                .Filter<MovementInitializationNeededTag>()
                .Inc<PlayerTag>()
                .End();
            
            _movementEnemiesInitFilter = World
                .Filter<MovementInitializationNeededTag>()
                .Inc<Enemy>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            InitializePlayerMovement();
            InitializeEnemiesMovement();
        }

        private void InitializePlayerMovement()
        {
            foreach (var player in _movementPlayerInitFilter)
            {
                var movementPool = World.GetPool<Movement>();
                movementPool.Add(player);
                ref var movement = ref movementPool.Get(player);
                movement.HorizontalSpeed = ConfigsProvider.GetPlayerConfig().Speed;
                
                World.GetPool<MovementInitializationNeededTag>().Del(player);
            }
        }
        
        private void InitializeEnemiesMovement()
        {
            foreach (var enemy in _movementEnemiesInitFilter)
            {
                var movementPool = World.GetPool<Movement>();
                movementPool.Add(enemy);
                ref var movement = ref movementPool.Get(enemy);
                var id = World.GetPool<Enemy>().Get(enemy).Id;
                movement.HorizontalSpeed = ConfigsProvider.GetEnemyConfig(id).Speed;
                
                World.GetPool<MovementInitializationNeededTag>().Del(enemy);
            }
        }
    }
}