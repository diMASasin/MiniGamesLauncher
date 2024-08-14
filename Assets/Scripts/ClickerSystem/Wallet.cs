using System;
using UnityEngine;

namespace ClickerSystem
{
    [Serializable]
    public class Wallet
    {
        public int Points { get; set; }
        
        public event Action<int> PointsValueChanged;
    
        public void Add(int value)
        {
            Points += value;
            PointsValueChanged?.Invoke(Points);
        }
    }
}