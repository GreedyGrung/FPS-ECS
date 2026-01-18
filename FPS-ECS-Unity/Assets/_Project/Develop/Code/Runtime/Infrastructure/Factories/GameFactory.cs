using Cysharp.Threading.Tasks;
using FpsEcs.Runtime.Configs.ScriptableObjects;
using FpsEcs.Runtime.Gameplay.Common;
using FpsEcs.Runtime.Infrastructure.Services.AssetManagement;
using FpsEcs.Runtime.Infrastructure.Services.Pools;
using FpsEcs.Runtime.Utils;
using FpsEcs.Runtime.Utils.Enums;
using LeoEcsLite.QoL.Factory;
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
        
        public GameObject CreateEmptyObjectWithName(string name) => new(name);
        
        public ObjectPool CreatePool(Transform parentTransform, PoolConfig poolConfig) =>
            new(poolConfig.Prefab,
                poolConfig.InitialSize,
                parentTransform, 
                poolConfig.IsAutoExpandable, 
                this,
                poolConfig.Type);

        public GameObject CreatePoolableObject(GameObject prefab, Transform parent, PoolId poolId, bool activeByDefault)
        {
            var gameObject = Object.Instantiate(prefab, parent);
            gameObject.GetComponent<Actor>().MarkPoolable(poolId);
            gameObject.SetActive(activeByDefault);

            return gameObject;
        }

        private int CreateEntity(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            var gameObject = Object.Instantiate(prefab, position, rotation);
            var entity = _entityFactory.Convert(gameObject);

            return entity;
        }
    }
}