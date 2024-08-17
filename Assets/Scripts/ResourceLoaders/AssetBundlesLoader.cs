using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace ResourceLoaders
{
    public class AssetBundlesLoadMethod : ILoadMethod
    {
        private readonly GameStaticData _staticData;

        public AssetBundlesLoadMethod(GameStaticData staticData)
        {
            _staticData = staticData;
        }
        
        public UnityWebRequest GetWebRequest(Task<Uri> task, string fileName)       
        {
            UnityWebRequest request =
                UnityWebRequestAssetBundle.GetAssetBundle(task.Result, 0).SendWebRequest().webRequest;
            
            return request;
        }

        public void OnResourceLoaded(UnityWebRequest request, string fileName)
        {
            if (_staticData.TryGetAssetBundle(fileName, out var bundle) == true)
                throw new ArgumentException("Data already loaded");

            AssetBundle newBundle = DownloadHandlerAssetBundle.GetContent(request);
            _staticData.AddAssetBundle(fileName, newBundle);
        }

        public void Unload(string fileName)
        {
            if (fileName == null || _staticData.TryGetAssetBundle(fileName, out var assetBundle) == false)
                throw new KeyNotFoundException("Data not loaded");

            if (assetBundle == null)
                throw new MissingReferenceException($"Data already unloaded");

            assetBundle.Unload(unloadAllLoadedObjects: true);
            _staticData.RemoveAssetBundle(fileName);

            Debug.Log($"Asset unloaded ({fileName})");
        }
    }
}