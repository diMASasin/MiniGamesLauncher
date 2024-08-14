using UnityEngine;

namespace RPG
{
    public class CharacterAnimations : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private CharacterMovement _movement;
        
        private static readonly int Direction = Animator.StringToHash("Direction");
        
        private IDiractionChangable _diractionChangable;

        public void OnEnable()
        {
            _movement.DirectionChanged += OnDirectionChanged;
        }

        private void OnDisable()
        {
            _movement.DirectionChanged -= OnDirectionChanged;
        }

        private void OnDirectionChanged(Vector3 direction)
        {
            _animator.SetFloat(Direction, direction.magnitude);
        }
    }
}