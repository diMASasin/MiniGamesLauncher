using System.Collections.Generic;
using Infrastructure;
using Infrastructure.SaveSystem;
using MainMenu;
using ResourceLoaders;
using UnityEngine;
using WalkingSimulator.Character;
using WalkingSimulator.Level;
using WalkingSimulator.Timers;
using WalkingSimulator.UI;
using WalkingSimulator.UI.Leaderboard;

namespace WalkingSimulator
{
    public class SimulatorBootstrapper : GameBootstrapper
    {
        [SerializeField] private FollowingCamera.FollowingCamera _followingCamera;
        [SerializeField] private WinCanvas _winCanvas;
        [SerializeField] private Canvas _gameplayCanvas;
        [SerializeField] private TimeView _timeView;
        [SerializeField] private CharacterMovement _characterMovement;
        [SerializeField] private CharacterAnimations _characterAnimations;
        [SerializeField] private Finish _finish;
        [SerializeField] private ExitToMainMenuButton _exitToMainMenuButton;
        [field: SerializeField] public List<AssetBandleObject> BundleObjectsInfo { get; private set; }
        
        private readonly OnExitProgressSaver _onExitProgressSaver = new();
        private readonly PlayerInput _input = new();
        private readonly GameStaticData _staticData = new();
        
        private Leaderboard _leaderboard;
        private Level.Level _level;
        private SaveSystem _saveSystem;

        public override void Init(GameConfig config, ResourceLoader resourceLoader, string nickname)
        {
            LoadAssets(_staticData, config);
            LoadSaves(config);
            InitializeObjects(config, resourceLoader, nickname);
            
            _level.Start();
        }

        private void OnDestroy()
        {
            if (_level != null) _level.Dispose();
        }

        private void LoadAssets(GameStaticData staticData, GameConfig config)
        {
            if (staticData.TryGetAssetBundle(config.BundleName, out var assetBundle) == false)
                throw new KeyNotFoundException(nameof(config.BundleName));
            
            foreach (var bandleObject in BundleObjectsInfo)
            {
                var asset = assetBundle.LoadAsset<GameObject>(bandleObject.Name);
                GameObject assetObject = Instantiate(asset, bandleObject.Parent);
                assetObject.name = assetObject.name.Replace("(Clone)", "");
            }
        }

        private void LoadSaves(GameConfig config)
        {
            _saveSystem = new SaveSystem(config.ProgressSavePath);

            if (_saveSystem.TryLoad(config.ProgressSavePath, out _leaderboard) == false)
                _leaderboard = new Leaderboard();
        }

        private void InitializeObjects(GameConfig gameConfig, ResourceLoader resourceLoader, string nickname)
        {
            _level = new Level.Level(_finish, _winCanvas, _gameplayCanvas, _timeView, _leaderboard, nickname);

            _characterMovement.Init(_input);
            _characterMovement.gameObject.SetActive(true);

            _followingCamera.SetTarget(_characterMovement.transform);
            
            _onExitProgressSaver.Init(_leaderboard, _saveSystem, resourceLoader, gameConfig);
            _exitToMainMenuButton.Init(_onExitProgressSaver);
        }
    }
}