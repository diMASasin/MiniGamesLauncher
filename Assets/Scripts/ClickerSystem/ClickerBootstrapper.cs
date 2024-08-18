using System.Collections.Generic;
using System.IO;
using ClickerSystem.Wallet;
using Infrastructure;
using Infrastructure.SaveSystem;
using MainMenu;
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
        [SerializeField] private ExitToMainMenuButton _exitToMainMenuButton;
        [field: SerializeField] public List<AssetBandleObject> BundleObjectsInfo { get; private set; }

        private readonly OnExitProgressSaver _onExitProgressSaver = new();
        private readonly Clicker _clicker = new();
        private Wallet.Wallet _wallet;
        private SaveSystem _saveSystem;

        public override void Init(GameConfig gameConfig, ResourceLoader resourceLoader, string nickname)
        {
            LoadAssets(new GameStaticData(), gameConfig);
            LoadSaves(gameConfig);
            InitializeObjects(gameConfig, resourceLoader);
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

            if (File.Exists(gameConfig.ProgressSavePath) == false || _saveSystem.TryLoad(gameConfig.ProgressSavePath, out _wallet) == false)
                _wallet = new Wallet.Wallet();
        }

        private void InitializeObjects(GameConfig gameConfig, ResourceLoader resourceLoader)
        {
            _walletView.Init(_wallet);
            _clicker.Init(_wallet, _pizzaButton);
            _onExitProgressSaver.Init(_wallet, _saveSystem, resourceLoader, gameConfig);
            _exitToMainMenuButton.Init(_onExitProgressSaver);
        }
    }
}