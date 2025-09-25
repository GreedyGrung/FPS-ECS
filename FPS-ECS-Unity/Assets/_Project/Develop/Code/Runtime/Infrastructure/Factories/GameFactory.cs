using Cysharp.Threading.Tasks;
using FpsEcs.Runtime.Infrastructure.Services.AssetManagement;
using FpsEcs.Runtime.Utils;
using UnityEngine;

namespace FpsEcs.Runtime.Infrastructure.Factories
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private GameObject _playerPrefab;
        private GameObject _enemyPrefab;

        public GameFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public async UniTask Load()
        {
            _playerPrefab = await _assetProvider.Load<GameObject>(Constants.Assets.PlayerPrefabPath);
            _enemyPrefab = await _assetProvider.Load<GameObject>(Constants.Assets.EnemyPrefabPath);
        }
        
        public GameObject CreatePlayer(Vector3 position, Quaternion rotation)
        {
            var gameObject = Object.Instantiate(_playerPrefab, position, rotation);
            
            return gameObject;
        }

        public GameObject CreateEnemy(Vector3 position, Quaternion rotation)
        {
            var gameObject = Object.Instantiate(_enemyPrefab, position, rotation);
            
            return gameObject;
        }
    }
}