using RPG.UI;
using Timers;
using UnityEngine;

namespace RPG
{
    public class Bootstrapper : MonoBehaviour, ICoroutinePerformer
    {
        [SerializeField] private FollowingCamera _followingCamera;
        [SerializeField] private CharacterMovement _characterMovement;
        [SerializeField] private CharacterAnimations _animations;
        [SerializeField] private Finish _finish;
        [SerializeField] private WinCanvas _winCanvas;
        [SerializeField] private TimeView _timeView;

        private readonly PlayerInput _input = new();
        private Level _level;
        
        private void Start()
        {
            _level = new Level(_finish, _winCanvas, this, _timeView);
            
            _characterMovement.Init(_input);
            _animations.Init(_characterMovement);
            
            _followingCamera.SetTarget(_characterMovement.transform);
            
            _level.Start();
        }

        private void OnDestroy()
        {
            _level.Dispose();
        }
    }
}