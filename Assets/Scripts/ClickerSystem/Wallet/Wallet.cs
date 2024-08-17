using System;

namespace ClickerSystem.Wallet
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