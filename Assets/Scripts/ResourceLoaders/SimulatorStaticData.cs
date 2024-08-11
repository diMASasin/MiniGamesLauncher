using System.Collections.Generic;
using UnityEngine;

namespace ResourceLoaders
{
    public class GameStaticData
    {
        private static readonly Dictionary<string, AssetBundle> _assetBundles = new();

        private static AssetBundle _assetBundle;

        public void AddAssetBundle(string name, AssetBundle assetBundle) => _assetBundles[name] = assetBundle;

        public bool TryGetAssetBundle(string name, out AssetBundle assetBundle) => 
            _assetBundles.TryGetValue(name, out assetBundle);
    }
}