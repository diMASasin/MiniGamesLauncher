using UnityEngine;

namespace ResourceLoaders
{
    public class GameStaticData
    {
        private static AssetBundle _assetBundle;

        public bool IsLoaded => _assetBundle != null;
        
        public AssetBundle AssetBundle => _assetBundle;
        
        public void SetData(AssetBundle assetBundle)
        {
            _assetBundle = assetBundle;
        }
    }
}