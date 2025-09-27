using FpsEcs.Runtime.Gameplay.ProgressionFeature.Components;
using FpsEcs.Runtime.Infrastructure.Factories;
using FpsEcs.Runtime.Infrastructure.Services.Configs;
using FpsEcs.Runtime.Infrastructure.Services.Input;
using FpsEcs.Runtime.Infrastructure.Services.Pause;
using FpsEcs.Runtime.Infrastructure.Services.Upgrades;
using FpsEcs.Runtime.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace FpsEcs.Runtime.Meta.Upgrades
{
    public class UpgradesView : UIPanelBase
    {
        private IPauseService _pauseService;
        private IInputService _inputService;
        private IUpgradesService _upgradesService;
        private IConfigsProvider _configsProvider;
        private IEntityFactory _entityFactory;

        [SerializeField] private TextMeshProUGUI _availablePoints;
        [SerializeField] private Button _applyButton;
        [SerializeField] private StatView _healthStat;
        [SerializeField] private StatView _damageStat;
        [SerializeField] private StatView _speedStat;
        [SerializeField] private Button _saveButton;

        [Inject]
        private void Construct(
            IPauseService pauseService,
            IInputService inputService,
            IUpgradesService upgradesService,
            IConfigsProvider configsProvider,
            IEntityFactory entityFactory)
        {
            _entityFactory = entityFactory;
            _configsProvider = configsProvider;
            _upgradesService = upgradesService;
            _inputService = inputService;
            _pauseService = pauseService;
        }

        protected override void Subscribe()
        {
            base.Subscribe();
            
            _applyButton.onClick.AddListener(ApplyUpgrades);
            _saveButton.onClick.AddListener(SaveProgress);
            
            _healthStat.OnAddPointButtonClicked += CheckForRemainingPoints;
            _damageStat.OnAddPointButtonClicked += CheckForRemainingPoints;
            _speedStat.OnAddPointButtonClicked += CheckForRemainingPoints;
        }

        protected override void Unsubscribe()
        {
            base.Unsubscribe();
            
            _applyButton.onClick.RemoveListener(ApplyUpgrades);
            
            _healthStat.OnAddPointButtonClicked -= CheckForRemainingPoints;
            _damageStat.OnAddPointButtonClicked -= CheckForRemainingPoints;
            _speedStat.OnAddPointButtonClicked -= CheckForRemainingPoints;
        }

        protected override void OnEnabled()
        {
            base.OnEnabled();
            
            _availablePoints.text = _upgradesService.AvailablePoints.ToString();

            var levels = _upgradesService.GetUpgradesLevels();
            
            _healthStat.Initialize(_upgradesService.AvailablePoints, levels.Health);
            _damageStat.Initialize(_upgradesService.AvailablePoints, levels.Damage);
            _speedStat.Initialize(_upgradesService.AvailablePoints, levels.Speed);
            
            CheckStatsLimits();
        }

        protected override void OnDisabled()
        {
            base.OnDisabled();
            
            _healthStat.Dispose();
            _damageStat.Dispose();
            _speedStat.Dispose();
        }

        protected override void Close()
        {
            base.Close();
            
            _pauseService.TogglePause();
            _inputService.SwitchInputMaps();
        }

        private void ApplyUpgrades()
        {
            var upgrades = new UpgradesData
            {
                Health = _healthStat.PendingPoints,
                Speed = _speedStat.PendingPoints,
                Damage = _damageStat.PendingPoints,
            };
            
            _upgradesService.Apply(upgrades);

            Close();
        }

        private void SaveProgress()
        {
            var entity = _entityFactory.Create();
            _entityFactory.World.GetPool<SaveProgressEvent>().Add(entity);
        }

        private void CheckForRemainingPoints()
        {
            int totalPendingPoints = _healthStat.PendingPoints + _damageStat.PendingPoints + _speedStat.PendingPoints;

            if (_upgradesService.AvailablePoints <= totalPendingPoints)
            {
                _healthStat.DisableButton();
                _damageStat.DisableButton();
                _speedStat.DisableButton();
            }
            
            int remainingPoints = _upgradesService.AvailablePoints - totalPendingPoints;
            _availablePoints.text = remainingPoints.ToString();
            
            CheckStatsLimits();
        }

        private void CheckStatsLimits()
        {
            var levels = _upgradesService.GetUpgradesLevels();
            var gameConfig = _configsProvider.GetGameConfig();

            CheckLimitsForStat(_healthStat, levels.Health, gameConfig.HealthBonusLimit);
            CheckLimitsForStat(_speedStat, levels.Speed, gameConfig.SpeedBonusLimit);
            CheckLimitsForStat(_damageStat, levels.Damage, gameConfig.DamageBonusLimit);
        }

        private void CheckLimitsForStat(StatView stat, int level,  int limit)
        {
            var totalHealthBonus = level + stat.PendingPoints;

            if (totalHealthBonus == limit)
            {
                stat.DisableButton();
            }
        }
    }
}