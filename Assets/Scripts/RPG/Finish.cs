using System;
using UnityEngine;

namespace RPG
{
    public class Finish : MonoBehaviour
    {
        public event Action Finished;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out CharacterMovement movement))
            {
                Finished?.Invoke();
            }
        }
    }
}