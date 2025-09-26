using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using FpsEcs.Runtime.Configs.Implementations;
using FpsEcs.Runtime.Configs.ScriptableObjects;
using FpsEcs.Runtime.Infrastructure.Services.AssetManagement;
using FpsEcs.Runtime.Utils;
using FpsEcs.Runtime.Utils.Enums;

namespace FpsEcs.Runtime.Infrastructure.Services.Configs
{
    public class ConfigsProvider : IConfigsProvider
    {
        private PlayerConfig _playerConfig;
        private GameConfig _gameConfig;
        private readonly Dictionary<EnemyId, EnemyConfig> _enemiesConfigs = new();
        private readonly Dictionary<WeaponId, WeaponConfig> _weaponsConfigs = new();
        private readonly IAssetProvider _assetProvider;

        public ConfigsProvider(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public async UniTask Load()
        {
            await LoadGameConfig();
            await LoadPlayerConfig();
            await LoadEnemiesConfigs();
            await LoadWeaponsConfigs();
        }

        public PlayerConfig GetPlayerConfig()
            => _playerConfig ?? throw new InvalidOperationException("PlayerConfig not loaded!");

        public GameConfig GetGameConfig()
            => _gameConfig ?? throw new InvalidOperationException("GameConfig not loaded!");
        public EnemyConfig GetEnemyConfig(EnemyId id)
            => _enemiesConfigs.TryGetValue(id, out var cfg)
                ? cfg
                : throw new KeyNotFoundException($"EnemyConfig for {id} not found.");

        public WeaponConfig GetWeaponConfig(WeaponId id)
            => _weaponsConfigs.TryGetValue(id, out var cfg)
                ? cfg
                : throw new KeyNotFoundException($"WeaponConfig for {id} not found.");

        public void Dispose()
        {
            _enemiesConfigs.Clear();
            _weaponsConfigs.Clear();
        }

        private async UniTask LoadGameConfig()
        {
            var result = await _assetProvider.Load<GameConfigSO>(Constants.Assets.GameConfigPath);
            _gameConfig = result.Config;
        }
        
        private async UniTask LoadPlayerConfig()
        {
            var result = await _assetProvider.Load<PlayerConfigSO>(Constants.Assets.PlayerConfigPath);
            _playerConfig = result.Config;
        }

        private async UniTask LoadEnemiesConfigs()
        {
            var result = await _assetProvider.LoadAll<EnemyConfigSO>(Constants.Assets.EnemiesConfigsPath);

            foreach (var config in result)
            {
                _enemiesConfigs.Add(config.Config.Id, config.Config);
            }
        }
        
        private async UniTask LoadWeaponsConfigs()
        {
            var result = await _assetProvider.LoadAll<WeaponConfigSO>(Constants.Assets.WeaponsConfigsPath);

            foreach (var config in result)
            {
                _weaponsConfigs.Add(config.Config.Id, config.Config);
            }
        }
    }
}