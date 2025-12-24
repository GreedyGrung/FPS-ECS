using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using FpsEcs.Runtime.Infrastructure.Services.AssetManagement;
using FpsEcs.Runtime.Meta.Upgrades;
using FpsEcs.Runtime.UI;
using FpsEcs.Runtime.Utils;
using FpsEcs.Runtime.Utils.Enums;
using LeoEcsLite.QoL.Factory;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace FpsEcs.Runtime.Infrastructure.Factories
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IObjectResolver _resolver;
        private readonly IEntityFactory _entityFactory;

        private Transform _uiRoot;
        private GameObject _hud;

        public UIFactory(IAssetProvider assetProvider, IObjectResolver resolver, IEntityFactory entityFactory)
        {
            _assetProvider = assetProvider;
            _resolver = resolver;
            _entityFactory = entityFactory;
        }

        public async UniTask Load()
        {
            _hud = await _assetProvider.Load<GameObject>(Constants.Assets.HudPath);
        }
        
        public async UniTask CreateUIRootAsync()
        {
            GameObject uiRootObject = await _assetProvider.Load<GameObject>(Constants.Assets.UIRootPath);
            GameObject uiRootPrefab = Object.Instantiate(uiRootObject);
            _uiRoot = uiRootPrefab.transform;
            _uiRoot.position = Vector3.zero;
            _uiRoot.localScale = Vector3.one;
        }
        
        public async UniTask<Dictionary<UIPanelId, UIPanelBase>> CreateUIPanelsAsync()
        {
            var dict = new Dictionary<UIPanelId, UIPanelBase>();

            var panel = await CreateUpgradesPanel();
            
            dict.Add(UIPanelId.Upgrades, panel);
            
            return dict;
        }

        public int CreateHud()
        {
            var gameObject = Object.Instantiate(_hud, _uiRoot);
            var entity = _entityFactory.Convert(gameObject);
            
            return entity;
        }

        private async UniTask<UIPanelBase> CreateUpgradesPanel()
        {
            var view = await _assetProvider.Load<UpgradesView>(Constants.Assets.UpgradesViewPath);
            var viewPrefab = _resolver.Instantiate(view, _uiRoot);
            
            return viewPrefab.GetComponent<UIPanelBase>();
        }
    }
}