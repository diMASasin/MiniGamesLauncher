using Firebase.Storage;
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
            var characterResource = staticData.AssetBundle.LoadAsset<GameObject>("HumanMale_Character_FREE");
            GameObject character = Instantiate(characterResource);
            _characterMovement = character.GetComponent<CharacterMovement>();
            _characterAnimations = character.GetComponent<CharacterAnimations>();

            GameObject finishResource = staticData.AssetBundle.LoadAsset<GameObject>("rpgpp_lt_building_04");
            _finish = Instantiate(finishResource).GetComponent<Finish>();
        }

        private void OnDestroy()
        {
            _level.Dispose();
        }
    }
}