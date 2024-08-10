using Firebase.Storage;
using ResourceLoaders;
using RPG.UI;
using Timers;
using UnityEngine;

namespace RPG
{
    public class Bootstrapper : MonoBehaviour, ICoroutinePerformer
    {
        [SerializeField] private FollowingCamera _followingCamera;
        [SerializeField] private WinCanvas _winCanvas;
        [SerializeField] private TimeView _timeView;

        private readonly PlayerInput _input = new();
        private Level _level;
        
        private void Awake()
        {
            GameObject character = Instantiate(WalkingSimulatiorResourceLoader.Character);
            var characterMovement = character.GetComponent<CharacterMovement>();
            var characterAnimations = character.GetComponent<CharacterAnimations>();
            
            Finish finish = Instantiate(WalkingSimulatiorResourceLoader.Building).GetComponent<Finish>();

            _level = new Level(finish, _winCanvas, this, _timeView);
            
            characterMovement.Init(_input);
            characterAnimations.Init(characterMovement);
            
            _followingCamera.SetTarget(characterMovement.transform);
            
            _level.Start();

            
            characterMovement.Init(_input);
        }

        private void OnDestroy()
        {
            _level.Dispose();
        }
    }
}