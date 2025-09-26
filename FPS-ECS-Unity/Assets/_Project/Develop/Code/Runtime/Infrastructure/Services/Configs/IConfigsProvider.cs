using System;
using Cysharp.Threading.Tasks;
using FpsEcs.Runtime.Configs.Implementations;
using FpsEcs.Runtime.Utils;
using FpsEcs.Runtime.Utils.Enums;

namespace FpsEcs.Runtime.Infrastructure.Services.Configs
{
    public interface IConfigsProvider : IDisposable
    {
        UniTask Load();
        PlayerConfig GetPlayerConfig();
        EnemyConfig GetEnemyConfig(EnemyId id);
        WeaponConfig GetWeaponConfig(WeaponId id);
        GameConfig GetGameConfig();
    }
}