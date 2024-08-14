using System.Collections.Generic;
using MainMenu_;
using ResourceLoaders;
using UnityEngine;
using UnityEngine.UI;

namespace ClickerSystem
{
    public class ClickerBootstrapper : GameBootstrapper
    {
        [SerializeField] private Canvas _gameplayCanvas;
        [SerializeField] private WalletView _walletView;
        [SerializeField] private Button _pizzaButton;
        [field: SerializeField] public List<AssetBandleObject> BundleObjectsInfo { get; private set; }

        private readonly Clicker _clicker = new();
        private Wallet _wallet;
        private SaveSystem _saveSystem;

        public override void Init(GameStaticData staticData, GameConfig gameConfig)
        {
            LoadAssets(staticData, gameConfig);
            LoadSaves(gameConfig);
            InitializeObjects();
        }

        private void LoadAssets(GameStaticData staticData, GameConfig gameConfig)
        {
            if (staticData.TryGetAssetBundle(gameConfig.BundleName, out var assetBundle) == false)
                throw new KeyNotFoundException(gameConfig.BundleName);

            foreach (var bandleObject in BundleObjectsInfo)
            {
                var pizzaResource = assetBundle.LoadAsset<GameObject>(bandleObject.Name);
                Instantiate(pizzaResource, bandleObject.Parent);
            }
        }

        private void LoadSaves(GameConfig gameConfig)
        {
            _saveSystem = new SaveSystem(gameConfig.ProgressSavePath);

            if (_saveSystem.TryLoad(gameConfig.ProgressSavePath, out _wallet) == false)
                _wallet = new Wallet();
        }

        private void InitializeObjects()
        {
            _walletView.Init(_wallet);
            _clicker.Init(_wallet, _pizzaButton);
        }

        private void OnApplicationQuit()
        {
            _saveSystem.Save(_wallet);
        }
    }
}