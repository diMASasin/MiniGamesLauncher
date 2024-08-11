using System;

namespace ClickerSystem
{
    public class Wallet
    {
        private int _points;

        public event Action<int> PointsValueChanged;
    
        public void Add(int value)
        {
            _points += value;
            PointsValueChanged?.Invoke(_points);
        }
    }
}