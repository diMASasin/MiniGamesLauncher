using UnityEngine;

namespace RPG
{
    public class CharacterAnimations : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        
        private static readonly int Direction = Animator.StringToHash("Direction");
        
        private IDiractionChangable _diractionChangable;

        public void Init(IDiractionChangable diractionChangable)
        {
            _diractionChangable = diractionChangable;

            _diractionChangable.DirectionChanged += OnDirectionChanged;
        }

        private void OnDestroy()
        {
            _diractionChangable.DirectionChanged -= OnDirectionChanged;
        }

        private void OnDirectionChanged(Vector3 direction)
        {
            _animator.SetFloat(Direction, direction.magnitude);
        }
    }
}