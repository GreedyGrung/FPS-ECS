using FpsEcs.Runtime.Gameplay.Enemies.Components;
using FpsEcs.Runtime.Gameplay.MovementLogic.Components;
using FpsEcs.Runtime.Gameplay.Player.Components;
using FpsEcs.Runtime.Infrastructure.Services.Configs;
using LeoEcsLite.QoL.Utils;
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
            _movementPlayerInitFilter = World.Inc<MovementInitializationNeededTag, PlayerTag>().End();
            _movementEnemiesInitFilter = World.Inc<MovementInitializationNeededTag, Enemy>().End();
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
                ref var movement = ref player.Add<Movement>();
                movement.HorizontalSpeed = ConfigsProvider.GetPlayerConfig().Speed;
                player.Del<MovementInitializationNeededTag>();
            }
        }
        
        private void InitializeEnemiesMovement()
        {
            foreach (var enemy in _movementEnemiesInitFilter)
            {
                ref var movement = ref enemy.Add<Movement>();
                var id = enemy.Get<Enemy>().Id;
                movement.HorizontalSpeed = ConfigsProvider.GetEnemyConfig(id).Speed;
                enemy.Del<MovementInitializationNeededTag>();
            }
        }
    }
}