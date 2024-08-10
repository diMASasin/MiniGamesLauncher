using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    private int _points;

    public event Action<int> PointsValueChanged;
    
    public void Add(int value)
    {
        _points += value;
        PointsValueChanged?.Invoke(_points);
    }
}