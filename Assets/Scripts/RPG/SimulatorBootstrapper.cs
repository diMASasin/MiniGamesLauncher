using System.Collections.Generic;
using ClickerSystem;
using MainMenu_;
using ResourceLoaders;
using RPG.UI;
using Timers;
using UnityEngine;

namespace RPG
{
    public class SimulatorBootstrapper : GameBootstrapper
    {
        [SerializeField] private FollowingCamera _followingCamera;
        [SerializeField] private WinCanvas _winCanvas;
        [SerializeField] private Canvas _gameplayCanvas;
        [SerializeField] private TimeView _timeView;
        [SerializeField] private CharacterMovement _characterMovement;
        [SerializeField] private CharacterAnimations _characterAnimations;
        [SerializeField] private Finish _finish;
        [SerializeField] private string _playerName;
        [field: SerializeField] public List<AssetBandleObject> BundleObjectsInfo { get; private set; }
        
        private readonly PlayerInput _input = new();
        private Leaderboard _leaderboard;
        private Level _level;
        private SaveSystem _saveSystem;

        public override void Init(GameStaticData staticData, GameConfig config)
        {
            LoadAssets(staticData, config);
            LoadSaves(config);
            InitializeObjects();

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

        private void InitializeObjects()
        {
            _level = new Level(_finish, _winCanvas, _gameplayCanvas, _timeView, _leaderboard, _playerName);

            _characterMovement.Init(_input);
            _characterMovement.gameObject.SetActive(true);

            _followingCamera.SetTarget(_characterMovement.transform);
        }

        private void OnApplicationQuit()
        {
            if (_saveSystem != null) _saveSystem.Save(_leaderboard);
        }
    }
}