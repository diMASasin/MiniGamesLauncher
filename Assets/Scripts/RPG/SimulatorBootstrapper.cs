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

        public const string SimulatorBundleName = "walkingsimulator";
        public const string CharacterObjectName = "HumanMale_Character_FREE";
        public const string BuildingObjectName = "rpgpp_lt_building_04";
        
        private readonly PlayerInput _input = new();
        private Level _level;
        private CharacterMovement _characterMovement;
        private CharacterAnimations _characterAnimations;
        private Finish _finish;

        public void Init(GameStaticData staticData)
        {
            LoadAssets(staticData);

            _level = new Level(_finish, _winCanvas, _gameplayCanvas, _timeView);
            
            _characterMovement.Init(_input);
            _characterAnimations.Init(_characterMovement);
            
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
            
            GameObject character = Instantiate(characterResource);
            _characterMovement = character.GetComponent<CharacterMovement>();
            _characterAnimations = character.GetComponent<CharacterAnimations>();

            _finish = Instantiate(finishResource).GetComponent<Finish>();
        }

        private void OnDestroy()
        {
            _level.Dispose();
        }
    }
}