using System;
using UnityEngine;

namespace RPG
{
    public interface IDiractionChangable
    {
        public event Action<Vector3> DirectionChanged;
    }
}