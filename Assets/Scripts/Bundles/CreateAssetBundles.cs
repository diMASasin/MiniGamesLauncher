using System.IO;
using UnityEditor;

namespace Bundles
{
    public class CreateAssetBundles
    {
        [MenuItem("Assets/Build AssetBundles")]
        private static void BuildAllAssetBundles()
        {
            const string assetBundleDirectory = "Assets/AssetBundles";
        
            if(Directory.Exists(assetBundleDirectory) == false) 
                Directory.CreateDirectory(assetBundleDirectory);

            BuildPipeline.BuildAssetBundles(assetBundleDirectory, 
                BuildAssetBundleOptions.None, 
                BuildTarget.StandaloneWindows);
        }
    }
}