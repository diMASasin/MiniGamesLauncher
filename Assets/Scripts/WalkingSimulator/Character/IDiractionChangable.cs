using System;
using UnityEngine;

namespace WalkingSimulator.Character
{
    public interface IDiractionChangable
    {
        public event Action<Vector3> DirectionChanged;
    }
}