using System.Collections.Generic;
using UnityEngine;

namespace ResourceLoaders
{
    public class GameStaticData
    {
        private static readonly Dictionary<string, AssetBundle> AssetBundles = new();

        public void AddAssetBundle(string name, AssetBundle assetBundle) => AssetBundles[name] = assetBundle;

        public bool TryGetAssetBundle(string name, out AssetBundle assetBundle) => 
            AssetBundles.TryGetValue(name, out assetBundle);

        public void RemoveAssetBundle(string key) => AssetBundles.Remove(key, out _);
    }
}