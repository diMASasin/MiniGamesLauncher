using System;
using UnityEngine;

namespace MainMenu_
{
    [Serializable]
    public class AssetBandleObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public Transform Parent { get; private set; }
    }
}