using System.Collections.Generic;
using ResourceLoaders;
using RPG.UI;
using Timers;
using UnityEngine;

namespace RPG
{
    public class SimulatorBootstrapper : MonoBehaviour
    {
        [SerializeField] private FollowingCamera _followingCamera;
        [SerializeField] private WinCanvas _winCanvas;
        [SerializeField] private Canvas _gameplayCanvas;
        [SerializeField] private TimeView _timeView;
        [SerializeField] private CharacterMovement _characterMovement;
        [SerializeField] private Finish _finish;

        public const string SimulatorBundleName = "walkingsimulator";
        public const string CharacterObjectName = "CharacterModel";
        public const string BuildingObjectName = "BuildingModel";
        
        private readonly PlayerInput _input = new();
        private Level _level;

        public void Init(GameStaticData staticData)
        {
            LoadAssets(staticData);

            _level = new Level(_finish, _winCanvas, _gameplayCanvas, _timeView);
            
            _characterMovement.Init(_input);
            
            _followingCamera.SetTarget(_characterMovement.transform);
            
            _level.Start();
            
            _characterMovement.Init(_input);
        }

        private void LoadAssets(GameStaticData staticData)
        {
            if (staticData.TryGetAssetBundle(SimulatorBundleName, out var assetBundle) == false)
                throw new KeyNotFoundException(nameof(SimulatorBundleName));

            var characterResource = assetBundle.LoadAsset<GameObject>(CharacterObjectName);
            var finishResource = assetBundle.LoadAsset<GameObject>(BuildingObjectName);

            Instantiate(characterResource, _characterMovement.transform)
                .TryGetComponent(out CharacterAnimations animations);
            animations.Init(_characterMovement);
            
            Instantiate(finishResource, _finish.transform);
        }

        private void OnDestroy()
        {
            _level.Dispose();
        }
    }
}