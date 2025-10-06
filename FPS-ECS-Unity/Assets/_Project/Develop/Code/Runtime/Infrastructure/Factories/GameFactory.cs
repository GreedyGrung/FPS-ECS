using Cysharp.Threading.Tasks;
using FpsEcs.Runtime.Infrastructure.Factories.Entities;
using FpsEcs.Runtime.Infrastructure.Services.AssetManagement;
using FpsEcs.Runtime.Utils;
using UnityEngine;

namespace FpsEcs.Runtime.Infrastructure.Factories
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IEntityFactory _entityFactory;
        
        private GameObject _playerPrefab;
        private GameObject _enemyPrefab;

        public GameFactory(IAssetProvider assetProvider, IEntityFactory entityFactory)
        {
            _assetProvider = assetProvider;
            _entityFactory = entityFactory;
        }

        public async UniTask Load()
        {
            _playerPrefab = await _assetProvider.Load<GameObject>(Constants.Assets.PlayerPrefabPath);
            _enemyPrefab = await _assetProvider.Load<GameObject>(Constants.Assets.EnemyPrefabPath);
        }

        public int CreatePlayer(Vector3 position, Quaternion rotation) 
            => CreateEntity(_playerPrefab, position, rotation);

        public int CreateEnemy(Vector3 position, Quaternion rotation) 
            => CreateEntity(_enemyPrefab, position, rotation);

        private int CreateEntity(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            var gameObject = Object.Instantiate(prefab, position, rotation);
            var entity = _entityFactory.Convert(gameObject);

            return entity;
        }
    }
}