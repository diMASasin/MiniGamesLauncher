using System;
using UnityEngine;
using WalkingSimulator.Character;

namespace WalkingSimulator.Level
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